using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ZirekService.Data;
using ZirekService.Models;
using ZirekService.Services;
using ZirekService.ViewModels;

namespace ZirekService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = RoleService.UserRole)]
    public class WordsNodesController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        public WordsNodesController(ApplicationDbContext context, AccountService accountService) {
            _context = context;
            _accountService = accountService;
        }

        [HttpGet("/GetWordsNodeById")]
        public IActionResult GetWordsNodeById(int id) {

            var account = _accountService.GetCurrentAccount();
            if (account == null)
                return null;

            var res = _context.WordsNodes.Where(s => s.Id == id && s.AccountId == account.Id).FirstOrDefault();

            if(res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpGet("/AddEnWordToNode")]
        public IActionResult AddEnWordToNode(int nodeId, int wordId) {

            var word = _context.EnWords.Where(s => s.Id == wordId).FirstOrDefault();
            if (word == null)
                return NotFound();

            var node = _context.WordsNodes.Where(s => s.Id == nodeId).FirstOrDefault();
            if (node == null)
                return NotFound();

            node.EnWords.Add(word);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/RemoveEnWordFromNode")]
        public IActionResult RemoveEnWordFromNode(int nodeId, int wordId) {

            var word = _context.EnWords.Where(s => s.Id == wordId).FirstOrDefault();
            if (word == null)
                return NotFound();

            var node = _context.WordsNodes.Where(s => s.Id == nodeId).FirstOrDefault();
            if (node == null)
                return NotFound();

            node.EnWords.Remove(word);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/GetWordsNodes")]
        public List<WordsNodeVM> GetWordsNodes(WordsNodeFilterVM filter) {
            IQueryable<WordsNodeEntity> nodes = _context.WordsNodes;

            var account = _accountService.GetCurrentAccount();
            if (account == null)
                return null;

            nodes = nodes.Where(s => s.AccountId == account.Id);

            if (!string.IsNullOrEmpty(filter.Name))
                nodes = nodes.Where(s => s.Name == filter.Name);

            return nodes.Select(s => new WordsNodeVM() { Name = s.Name, Id = s.Id, IsPublic = s.IsPublic} ).ToList();
        }

        [HttpPut("/EditWordsNode")]
        public IActionResult EditWordsNode(WordsNodeVM node) {
            var nodeEntity = _context.WordsNodes.Where(s => s.Id == node.Id).FirstOrDefault();
            if (nodeEntity == null)
                return NotFound();
            nodeEntity.Name = node.Name;
            nodeEntity.IsPublic = node.IsPublic;
            _context.WordsNodes.Update(nodeEntity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("/DeleteWordsNode")]
        public IActionResult DeleteWordsNode(int id) {
            var nodeEntity = _context.WordsNodes.Where(s => s.Id == id).FirstOrDefault();
            if (nodeEntity == null)
                return NotFound();
            _context.WordsNodes.Remove(nodeEntity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/CreateWordsNode")]
        public IActionResult CreateWordsNode(CreateWordsNodeVM node) {
            var account = _accountService.GetCurrentAccount();
            if (account == null)
                throw new NullReferenceException("WordsNodesController: account=null");
            var newNode = new WordsNodeEntity() {
                Name = node.Name,
                IsPublic = node.IsPublic,
                AccountId = account.Id
            };

            _context.WordsNodes.Add(newNode);
            _context.SaveChanges();
            return Ok();
        }
    }
}
