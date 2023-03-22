namespace ZirekService.Models.WordsNodes
{
    public class WordsNodeCreate
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
