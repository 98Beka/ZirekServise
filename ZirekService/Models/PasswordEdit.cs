using System.ComponentModel.DataAnnotations;

namespace ZirekService.Models
{
    public class ChangePassword
    {
        public string Id { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
