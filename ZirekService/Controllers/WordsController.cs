using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZirekService.Data;
using ZirekService.Models;
using ZirekService.Services;
using ZirekService.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZirekService.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class WordsController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountEntity;
        public WordsController(ApplicationDbContext context, AccountService accountService) {
            _context = context;
            _accountEntity = accountService;
        }

        //private IQueryable<EnWordEntity> GetFiltratedWords(WordsFilter wordsFilter) {
        //    var words = _context.EnWords.Where(s => s.Ac.Any(s => s.Id == wordsFilter.AccountId));
        //    return words;
        //}

        //[HttpGet("/ShowWords")]
        //public async Task<List<EnWordEntity>> ShowWords(WordsFilter wordsFilter) {
        //    var words = GetFiltratedWords(wordsFilter);
        //    return await words.Include(s => s.RuWordEnitys).ToListAsync();
        //}

        //[HttpGet("/GetWordsFirstLevel")]
        //public List<string> GetWordsFirstLevel(int lenth) {
        //    return _context.RuWords.;
        //}

    }
}
