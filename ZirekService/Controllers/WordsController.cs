using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZirekService.Data;
using ZirekService.Models;
using ZirekService.Services;
using ZirekService.ViewModels;

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

        [HttpPost("/CreateEnWord")]
        public IActionResult CreateEnWord(CreateEnWordVM word) {
            _context.RuWords.AddRange(word.RuWords);
            _context.EnWords.Add(new EnWordEntity() {
                Priority = 100,
                Value = word.Value,
                RuWords = word.RuWords,
                Type = word.Type
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/GetEnWordById")]
        public IActionResult GetEnWordById(int id) {
            var res = _context.EnWords.Where(s => s.Id == id).FirstOrDefault();
            if (res == null)
                return NotFound();

            return Ok(res);
        }

        [HttpPost("/GetEnWords")]
        public List<EnWordEntity> GetEnWords(WordsFilterVM filterVM) {
            IQueryable<EnWordEntity> words = _context.EnWords;
            if (!string.IsNullOrEmpty(filterVM.Value))
                words = words.Where(s => s.Value == filterVM.Value);
            return words.ToList();
        }

        [HttpPost("/EditEnWord")]
        public IActionResult EditEnWord(EditEnWordVM word) {
            var wordEntity = _context.EnWords.Where(s => s.Id == word.Id).FirstOrDefault();
            if (word == null)
                return NotFound();
            wordEntity.RuWords = word.RuWords;
            wordEntity.Value = word.Value;
            _context.Update(wordEntity);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("/DeleteEnWord")]
        public IActionResult DeleteEnWord(int id) {
            var word = _context.EnWords.Where(s => s.Id == id).FirstOrDefault();
            if (word == null)
                return NotFound();
            _context.EnWords.Remove(word);
            _context.SaveChanges();
            return Ok();
        }


    }
}
