using System.ComponentModel.DataAnnotations;

namespace ZirekService.Models.User
{
    public class UserLogin {
        [Required(ErrorMessage = "User Name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
