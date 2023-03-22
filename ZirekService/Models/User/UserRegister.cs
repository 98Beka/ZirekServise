using System.ComponentModel.DataAnnotations;

namespace ZirekService.Models.User
{
    public class UserRegister {
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
