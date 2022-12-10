using System.ComponentModel.DataAnnotations;

namespace ZirekTeamAdmin.ViewModels {
    public class UserCreateViewModel {
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
