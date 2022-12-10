using Microsoft.AspNetCore.Identity;

namespace ZirekService.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
