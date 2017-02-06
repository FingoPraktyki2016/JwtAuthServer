using System.ComponentModel.DataAnnotations;
using LegnicaIT.DataAccess.Enums;

namespace LegnicaIT.DataAccess.Models
{
    public class UserApps : BaseEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        public App App { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
