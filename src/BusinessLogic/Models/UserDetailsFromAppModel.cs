using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Models
{
    public class UserDetailsFromAppModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }

}
