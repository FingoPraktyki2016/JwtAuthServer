
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.JwtManager.Models
{
    public class AppUserViewModel
    {
        public int UserId { get; set; }

        public int AppId { get; set; }

        public UserRole Role { get; set; }
    }

}
