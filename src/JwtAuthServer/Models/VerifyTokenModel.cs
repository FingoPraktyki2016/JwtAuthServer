using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class VerifyTokenModel
    {
        [Required]
        public string Token { get; set; }
    }
}
