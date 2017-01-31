
using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models;
using System.Collections.Generic;

namespace LegnicaIT.BusinessLogic.Actions.App.Interfaces
{
    public interface IGetUserApps : IAction

    {
        List<AppModel>Invoke (int userId);
    }
}
