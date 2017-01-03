using System;
using LegnicaIT.BusinessLogic.Models.Base;

namespace LegnicaIT.BusinessLogic.Models.User
{
    public class UserModel : BaseModel
    {
        public string Email { set; get; }

        public DateTime EmailConfirmedOn { set; get; }

        public string Name { set; get; }

        public DateTime LockedOn { set; get; }
    }
}