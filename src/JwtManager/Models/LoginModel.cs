﻿using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtManager.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string AppId { get; set; }
    }
}
