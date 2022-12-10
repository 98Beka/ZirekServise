using Microsoft.AspNetCore.Identity;
using ZirekService.Data;

namespace ZirekService.Services {
    public class RoleService {
        private readonly ApplicationDbContext context;
        public const string AdminRole = "admin";
        public const string UserRole = "user";
        private string adminRoleId;
        private string userRoleId;
        public RoleService(ApplicationDbContext context) {
            this.context = context;
            adminRoleId = context.Roles.Where(r => r.Name == AdminRole).Select(s => s.Id).FirstOrDefault() ?? string.Empty;
            userRoleId = context.Roles.Where(r => r.Name == UserRole).Select(s => s.Id).FirstOrDefault() ?? string.Empty;
        }

        public string GetRole(string id) {
            return context.UserRoles.Any(s => s.UserId == id && s.RoleId == adminRoleId) ? AdminRole :
                context.UserRoles.Any(s => s.UserId == id && s.RoleId == userRoleId) ? UserRole : "none role";
        }
    }

}
