using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtManager.Models.Auth
{
    public class ResendEmailConfirmationViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
