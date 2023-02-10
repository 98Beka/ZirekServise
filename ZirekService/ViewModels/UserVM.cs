using Microsoft.AspNetCore.Identity;

namespace ZirekService.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
