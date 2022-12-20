namespace ZirekService.Models {
    public class EnWordEntity {
        public int Id { get; set; }
        public string Value { get; set; }
        public int Priority { get; set; }
        public List<RuWordEnity> RuWords{ get; set; }
    }
}
