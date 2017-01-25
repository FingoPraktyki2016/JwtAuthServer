﻿using System;
using LegnicaIT.BusinessLogic.Models.Base;

namespace LegnicaIT.BusinessLogic.Models.User
{
    public class UserModel : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime EmailConfirmedOn { get; set; }

        public string Name { get; set; }

        public DateTime LockedOn { get; set; }

        public override bool IsValid()
        {
            return !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password));
        }
    }
}