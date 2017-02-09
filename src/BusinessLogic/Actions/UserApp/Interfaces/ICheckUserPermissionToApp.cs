using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces
{
    public interface ICheckUserPermissionToApp : IAction
    {
        bool Invoke(int userId, int appId, ActionType type = ActionType.Display);
    }
}
