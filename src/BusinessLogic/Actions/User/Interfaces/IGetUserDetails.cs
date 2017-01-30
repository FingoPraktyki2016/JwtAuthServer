using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetUserDetails : IAction
    {
        UserModel Invoke(string email);
    }
}