using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtManager.Models
{
    public class EditUserDetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}