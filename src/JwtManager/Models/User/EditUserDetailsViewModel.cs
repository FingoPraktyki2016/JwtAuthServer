using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtManager.Models.User
{
    public class EditUserDetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string Name { get; set; }
    }
}