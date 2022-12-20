namespace ZirekService.Models {
    public class WordsNodeEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<KeyWordEntity> KeyWords { get; set; }
        public List<EnWordEntity> EnWords { get; set; }
        public int AccountId { get; set; }
        public AccountEntity Account { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
