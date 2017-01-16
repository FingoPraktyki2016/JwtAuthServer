using System.ComponentModel.DataAnnotations;

namespace LegnicaIT.DataAccess.Models
{
    public class UserApps : BaseEntity
    {
        [Required]
        public User User { set; get; }

        [Required]
        public App App { set; get; }
    }
}
