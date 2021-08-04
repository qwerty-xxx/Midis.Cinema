﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Login should be 4 symbols at least")]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}