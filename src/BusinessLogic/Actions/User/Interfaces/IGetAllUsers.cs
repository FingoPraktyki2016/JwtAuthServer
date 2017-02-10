using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models;
using System.Collections.Generic;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetAllUsers : IAction
    {
        List<UserModel> Invoke();
    }
}
