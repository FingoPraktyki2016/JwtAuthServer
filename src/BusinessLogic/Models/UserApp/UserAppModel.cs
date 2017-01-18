using LegnicaIT.BusinessLogic.Models.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Models.UserApp
{
    public class UserAppModel : BaseModel
    {
        public int UserId;
        public int AppId;
        public UserRole Role;
    }
}