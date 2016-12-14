using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int AppId { get; set; }
    }
}
