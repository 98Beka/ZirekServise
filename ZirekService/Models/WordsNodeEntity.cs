namespace ZirekService.Models {
    public class WordsNodeEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<KeyWordEntity> KeyWords { get; set; } = new List<KeyWordEntity>();
        public List<EnWordEntity> EnWords { get; set; } = new List<EnWordEntity>();
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
