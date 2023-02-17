﻿using System.ComponentModel.DataAnnotations;

namespace ZirekService.ViewModels {
    public class RegisterVM {
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
