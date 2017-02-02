﻿using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGrantRole : IAction
    {
        bool Invoke(int appId, int user, UserRole newRole);
    }
}
