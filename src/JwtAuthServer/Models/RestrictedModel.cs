using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtAuthServer.Models
{
    public class RestrictedModel
    {
        [Required]
        public string Data { get; set; }
    }
}
