using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ZirekService.Models {
    public class BaseEntity {
        public long Id { get; set; }
        [Display(Name = "Кем создан")]
        public string? CreatedBy { get; set; }
        [Display(Name = "Когда создан")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Кем обновлен")]
        public string? UpdatedBy { get; set; }
        [Display(Name = "Когда обновлен")]
        public DateTime? UpdatedAt { get; set; }
    }

}
