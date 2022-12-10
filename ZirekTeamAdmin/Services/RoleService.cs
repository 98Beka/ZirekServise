using Microsoft.AspNetCore.Identity;
using ZirekTeamAdmin.Data;

namespace ZirekTeamAdmin.Services {
    public class RoleService {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        public const string AdminRole = "admin";
        public const string UserRole = "user";
        private string adminRoleId;
        private string userRoleId;
        public RoleService(ApplicationDbContext context, UserManager<IdentityUser> userManager) {
            this.context = context;
            this.userManager = userManager;
            adminRoleId = context.Roles.Where(r => r.Name == AdminRole).Select(s => s.Id).FirstOrDefault() ?? string.Empty;
            userRoleId = context.Roles.Where(r => r.Name == UserRole).Select(s => s.Id).FirstOrDefault() ?? string.Empty;
        }

        public string GetRole(string id) {
            return context.UserRoles.Any(s => s.UserId == id && s.RoleId == adminRoleId) ? AdminRole :
                context.UserRoles.Any(s => s.UserId == id && s.RoleId == userRoleId) ? UserRole : "none role";
        }

        public async Task AddRolesAsync(IdentityUser identityUser, string role) {
            switch (role) {
                case UserRole:
                    await userManager.AddToRoleAsync(identityUser, UserRole);
                    return;
                case AdminRole:
                    await userManager.AddToRoleAsync(identityUser, UserRole);
                    await userManager.AddToRoleAsync(identityUser, AdminRole);
                    return;
            }
        }
    }

}
