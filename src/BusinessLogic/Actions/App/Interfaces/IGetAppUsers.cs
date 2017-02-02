using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models;
using System.Collections.Generic;

namespace LegnicaIT.BusinessLogic.Actions.App.Interfaces
{
    public interface IGetAppUsers :IAction
    {
        List<UserDetailsFromAppModel> Invoke(int appId);
    }
}
