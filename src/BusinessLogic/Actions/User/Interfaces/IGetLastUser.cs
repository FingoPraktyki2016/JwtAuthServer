using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.User;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetLastUser : IAction
    {
        UserModel Invoke(UserModel user);
    }
}