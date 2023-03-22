using System.ComponentModel.DataAnnotations;

namespace ZirekService.Models.User
{
    public class UserCreate
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
