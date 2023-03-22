using Microsoft.AspNetCore.Identity;

namespace ZirekService.Models.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int level { get; set; }
        public List<WordsNodeEntity> wordsNodes { get; set; } = new List<WordsNodeEntity>();
        public string IdentityUserId { get; set; }
    }
}
