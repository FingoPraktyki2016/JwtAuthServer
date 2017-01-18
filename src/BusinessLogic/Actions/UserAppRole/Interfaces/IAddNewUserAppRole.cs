using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.UserAppRole;

namespace LegnicaIT.BusinessLogic.Actions.UserAppRole.Interfaces
{
    public interface IAddNewUserAppRole : IAction
    {
        void Invoke(int userId, int appId, int roleId);
    }
}