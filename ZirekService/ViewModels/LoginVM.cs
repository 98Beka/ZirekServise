﻿using System.ComponentModel.DataAnnotations;

namespace ZirekService.ViewModels {
    public class LoginVM {
        [Required(ErrorMessage = "User Name is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
