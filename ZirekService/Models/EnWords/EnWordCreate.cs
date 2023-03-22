using ZirekService.Models.Entities;
using ZirekService.Models.RuWords;

namespace ZirekService.Models.EnWords
{
    public class EnWordCreate
    {
        public string Value { get; set; }
        public string Type { get; set; }
        public List<RuWordCreate> RuWords { get; set; } = new List<RuWordCreate>();
    }
}
