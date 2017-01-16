using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.DataAccess.Models
{
    public class UserAppRole : BaseEntity
    {
        [Required]
        public UserApps User { set; get; }

        [Required]
        public App App { set; get; }

        [Required]
        public Role Role { set; get; }
    }
}
