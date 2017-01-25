using System;
using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AppId must be a valid number")]
        public int AppId { get; set; }
    }
}
