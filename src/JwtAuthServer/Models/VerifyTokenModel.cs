using System.ComponentModel.DataAnnotations;

namespace JwtAuthServer.Models
{
    public class VerifyTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
