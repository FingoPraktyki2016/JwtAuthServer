
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.JwtManager.Models
{
    public class UserDetailsFromAppViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
