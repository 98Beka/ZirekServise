using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZirekService.Data;
using ZirekService.Models;
using ZirekService.Services;
using ZirekService.ViewModels;

namespace ZirekService.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class WordsNodesController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        public WordsNodesController(ApplicationDbContext context, AccountService accountService) {
            _context = context;
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Create(WordsNodeViewModel node) {
            var account = _accountService.GetCurrentAccount();
            if (account == null)
                throw new NullReferenceException("account=null");
            var newNode = new WordsNodeEntity() {
                Name = node.Name,
                IsPublic = node.IsPublic,
                AccountId = account.Id
            };

            _context.WordsNodes.Add(newNode);
            return Ok();
        }
    }
}
