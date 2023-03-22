namespace ZirekService.Models.Entities
{
    public class WordsNodeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();
        public List<EnWordEntity> EnWords { get; set; } = new List<EnWordEntity>();
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
