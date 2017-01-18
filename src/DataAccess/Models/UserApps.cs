using System.ComponentModel.DataAnnotations;
using LegnicaIT.DataAccess.Enums;

namespace LegnicaIT.DataAccess.Models
{
    public class UserApps : BaseEntity
    {
        [Required]
        public User User { set; get; }

        [Required]
        public App App { set; get; }

        [Required]
        public UserRole Role { set; get; }
    }
}
