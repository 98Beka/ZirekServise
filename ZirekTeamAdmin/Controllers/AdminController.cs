using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZirekTeamAdmin.Data;
using ZirekTeamAdmin.Services;
using ZirekTeamAdmin.ViewModels;

namespace ZirekTeamAdmin.Controllers {

    [Authorize(Roles = RoleService.AdminRole)]
    public class AdminController : Controller {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly RoleService roleService;


        public AdminController(UserManager<IdentityUser> userManager, ApplicationDbContext context, RoleService roleService) {
            this.userManager = userManager;
            this.context = context;
            this.roleService = roleService;

        }

        public async Task<IActionResult> Index() {
            var currentUserId = context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.CurrentUserId = currentUserId.Id;
            IEnumerable<UserViewModel> users = context.Users.ToList().Select(u => new UserViewModel() {
                Role = roleService.GetRole(u.Id),
                Id = u.Id,
                Email = u.Email
            });

            return View(users);
        }

        [HttpGet]
        public IActionResult Create() {
            ViewData["Roles"] = new SelectList(context.Roles, "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel user) {
            if (ModelState.IsValid) {
                if (await userManager.FindByNameAsync(user.Email) == null) {
                    IdentityUser identityUser = new IdentityUser { Email = user.Email, UserName = user.Email };
                    IdentityResult result = await userManager.CreateAsync(identityUser, user.Password);

                    if (result.Succeeded) {
                        await roleService.AddRolesAsync(identityUser, user.Role);
                        return RedirectToAction(nameof(Index));
                    } else foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);

                } else ModelState.AddModelError(string.Empty, "Этот пользователь уже существует");

            } else ModelState.AddModelError(string.Empty, "Ошибка валидации страницы");
            ViewData["Roles"] = new SelectList(context.Roles, "Name");
            return View(user);
        }

        public async Task<IActionResult> Delete(string? id) {
            if (id == null)
                return NotFound();

            IdentityUser user = await userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            return View(new DeleteUserViewModel { Id = user.Id, Email = user.Email, Role = roleService.GetRole(user.Id) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            IdentityUser user = await userManager.FindByIdAsync(id);
            if (user != null) {
                IdentityResult result = await userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

    }

}
