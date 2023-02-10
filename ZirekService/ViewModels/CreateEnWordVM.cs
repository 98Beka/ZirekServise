using ZirekService.Models;

namespace ZirekService.ViewModels
{
    public class CreateEnWordVM
    {
        public string Value { get; set; }
        public string? Type { get; set; }
        public List<RuWordEnity> RuWords { get; set; } = new List<RuWordEnity>();
    }
}
