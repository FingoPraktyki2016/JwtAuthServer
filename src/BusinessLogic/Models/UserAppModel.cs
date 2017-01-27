using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Models
{
    public class UserAppModel : BaseModel
    {
        public int UserId;
        public int AppId;
        public UserRole Role;
    }
}
