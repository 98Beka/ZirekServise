using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZirekService.Data;
using ZirekService.Models;

namespace ZirekService.Services {
    public class AccountService {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccountEntity> GetCurrentAccountAsync() {
            var IdentityUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return await _context.Accounts.FirstOrDefaultAsync(s => s.IdentityUserId == IdentityUser.Id);
        }

        public AccountEntity GetCurrentAccount() {
            var IdentityUserId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _context.Accounts.FirstOrDefault(s => s.IdentityUserId == IdentityUserId);
        }

    }
}
