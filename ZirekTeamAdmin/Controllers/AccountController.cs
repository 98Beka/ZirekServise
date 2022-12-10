using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using ZirekTeamAdmin.Data;
using ZirekTeamAdmin.ViewModels;

namespace ZirekTeamAdmin.Controllers {
    public class AccountController : Controller {

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context) {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var result =
                    await signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
                if (result.Succeeded) {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
                        return Redirect(model.ReturnUrl);
                    } else {
                        return RedirectToAction("Index", "Entities");
                    }
                } else {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Entities");
        }

        public IActionResult AccessDenied() {
            return View();
        }

        public async Task<IActionResult> ChangePassword() {
            var currentUserId = context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            if (currentUserId == null)
                return NotFound();
            IdentityUser user = await userManager.FindByIdAsync(currentUserId.Id);
            if (user == null) {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) {
            if (ModelState.IsValid) {
                IdentityUser user = await userManager.FindByIdAsync(model.Id);
                if (user != null) {
                    var _passwordValidator =
                    HttpContext.RequestServices.GetService(typeof(IPasswordValidator<IdentityUser>)) as IPasswordValidator<IdentityUser>;
                    var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<IdentityUser>)) as IPasswordHasher<IdentityUser>;
                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(userManager, user, model.Password);
                    if (result.Succeeded) {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                        await userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "Entities");
                    } else {
                        foreach (var error in result.Errors) {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                } else {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return RedirectToAction("Index", "Entities");
        }
    }

}
