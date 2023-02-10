using Microsoft.AspNetCore.Identity;
using ZirekService.Services;

namespace ZirekService.Data {
    public class RoleInitializer {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync(RoleService.UserRole) == null) {
                await roleManager.CreateAsync(new IdentityRole(RoleService.UserRole));
            }
            if (await roleManager.FindByNameAsync(RoleService.AdminRole) == null) {
                await roleManager.CreateAsync(new IdentityRole(RoleService.AdminRole));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null) {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(admin, RoleService.UserRole);
                    await userManager.AddToRoleAsync(admin, RoleService.AdminRole);
                }
            }
        }
    }

}
