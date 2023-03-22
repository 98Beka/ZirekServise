using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZirekService.Data;
using ZirekService.Models.Entities;
using ZirekService.Models.EnWords;
using ZirekService.Models.RuWords;
using ZirekService.Services;
using static NpgsqlTypes.NpgsqlTsQuery;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZirekService.Controllers {
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = RoleService.UserRole)]
    public class WordsController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountEntity;
        public WordsController(ApplicationDbContext context, AccountService accountService) {
            _context = context;
            _accountEntity = accountService;
        }
        #region WordType

        #endregion

        #region EnWord
        [HttpPost("/CreateEnWord")]
        public IActionResult CreateEnWord(EnWordCreate word) {
            var tag = _context.Tags.Where(x => x.Value == word.Type).FirstOrDefault();
            if (tag == null)
                return BadRequest($"type :{word.Type} doesn't exist");

            var enWord = new EnWordEntity() {
                Priority = 100,
                Value = word.Value,
                TagId = tag.Id
            };
            _context.EnWords.Add(enWord);
            _context.RuWords.AddRange(word.RuWords.Select(s =>
                new RuWordEnity() {
                    Value = s.Value,
                    EnWordId = enWord.Id
                }
                ));
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("/CreateEnWordAndBind")]
        public IActionResult CreateEnWordAndBind(EnWordCreateAndBind word) {
            var tag = _context.Tags.Where(x => x.Value == word.Type).FirstOrDefault();
            if (tag == null)
                return BadRequest($"tag :{word.Type} doesn't exist");

            var enWord = new EnWordEntity() {
                Priority = 100,
                Value = word.Value,
                TagId = tag.Id
            };
            _context.EnWords.Add(enWord);
            _context.RuWords.AddRange(word.RuWords.Select(s =>
                new RuWordEnity() {
                    Value = s.Value,
                    EnWordId = enWord.Id
                }
                ));
            var node = _context.WordsNodes.Where(s => s.Id == word.NodeId).FirstOrDefault();
            if (node == null)
                return NotFound();
            node.EnWords.Add(enWord);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/GetEnWordById")]
        public IActionResult GetEnWordById(long id) {
            var res = _context.EnWords.Where(s => s.Id == id).FirstOrDefault();
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpPost("/GetEnWords")]
        public IActionResult GetEnWords(EnWordsFilter filterVM) {
            IQueryable<EnWordEntity> words = _context.EnWords;
            if (!string.IsNullOrEmpty(filterVM.Value))
                words = words.Where(s => s.Value == filterVM.Value);
            return Ok(words.ToList());
        }

        [HttpPut("/EditEnWord")]
        public IActionResult EditEnWord(EnWordEdit word) {
            var tag = _context.Tags.Where(x => x.Value == word.Type).FirstOrDefault();
            if (tag == null)
                return BadRequest($"tag :{word.Type} doesn't exist");
            var wordEntity = _context.EnWords.Where(s => s.Id == word.Id).FirstOrDefault();
            if (wordEntity == null)
                return BadRequest($"word :{word.Value} doesn't exist");
            wordEntity.Value = word.Value;
            wordEntity.TagId = tag.Id;
            _context.Update(wordEntity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/DeleteEnWord")]
        public IActionResult DeleteEnWord(long id) {
            var word = _context.EnWords.Where(s => s.Id == id).FirstOrDefault();
            if (word == null)
                return NotFound();
            _context.EnWords.Remove(word);
            _context.SaveChanges();
            return Ok();
        }
        #endregion

        #region RuWord
        [HttpPost("/CreateRuWord")]
        public IActionResult CreateRuWord(RuWordCreate word, long enWordId) {
            var enWord = _context.EnWords.Where(s => s.Id == enWordId).FirstOrDefault();
            if (enWord == null)
                return BadRequest("EnWord doesn't exist");
            _context.RuWords.Add(new RuWordEnity() { EnWordId = enWord.Id, Value = word.Value });
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/GetRuWord")]
        public IActionResult GetRuWord(long enWordId) {
            var enWord = _context.EnWords.Where(s => s.Id == enWordId).FirstOrDefault();
            if (enWord == null)
                return BadRequest("EnWord doesn't exist");
            return Ok(_context.RuWords.Where(s => s.EnWordId == enWordId).ToList());
        }

        [HttpDelete("/DeleteRuWord")]
        public IActionResult DeleteRuWord(long ruWordId) {
            var ruWord = _context.RuWords.Where(s => s.Id == ruWordId).FirstOrDefault();
            if (ruWord == null)
                return NotFound();
            _context.RuWords.Remove(ruWord);
            _context.SaveChanges();
            return Ok();
        }
        #endregion

    }
}
