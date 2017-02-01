using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.JwtManager.Models
{
    public class AppViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
