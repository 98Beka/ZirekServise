using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZirekService.Services;
using ZirekService.ViewModels;

namespace ZirekService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : Controller {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration configuration ) {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> Login(LoginVM model) {
            model.Email = model.Email.ToLower().Trim();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) {
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                    return BadRequest("Wrong password");
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles) {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized($"User '{model.Email}' hasn't found");
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterVM model) {
            model.Email = model.Email.ToLower().Trim();
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseVM { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new() {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);
            else
                await _userManager.AddToRoleAsync(user, RoleService.UserRole);
            return Ok(new ResponseVM { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet("/GetUserInfor")]
        [Authorize(Roles = RoleService.UserRole)]
        public async Task<IActionResult> GetUserInfo(string email) {
            email = email.ToLower().Trim();
            if (string.IsNullOrEmpty(email))
                return BadRequest("email is empty");
            var user = await _userManager.Users.Where(x => x.Email.ToLower().Trim() == email).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();
            return Ok(new UserRPVM() { UserName = user.UserName });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims) {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
