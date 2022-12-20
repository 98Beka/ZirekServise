using Microsoft.AspNetCore.Http;
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

        private AccountEntity CreateAccount() {
            var userId = _context.Users.Where(s => s.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).Select(s => s.Id).FirstOrDefault();
            if (userId == null)
                throw new ArgumentNullException("AccountService userId = null or user not found");
            var account = new AccountEntity() {
                IdentityUserId = userId,
                level = 0,
            };
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public async Task<AccountEntity> GetCurrentAccountAsync() {
            var userId = _context.Users.Where(s => s.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).Select(s => s.Id).FirstOrDefaultAsync()?.Result;
            var res = await _context.Accounts.FirstOrDefaultAsync(s => s.IdentityUserId == userId);
            return res ?? CreateAccount();
        }

        public AccountEntity GetCurrentAccount() {
            var userId = _context.Users.Where(s => s.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).Select(s => s.Id).FirstOrDefault();
            var res = _context.Accounts.FirstOrDefault(s => s.IdentityUserId == userId);
            return res ?? CreateAccount();
        }

    }
}
