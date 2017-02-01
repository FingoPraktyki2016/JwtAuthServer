using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface ICheckUserPermission : IAction
    {
        bool Invoke(int requestorId, int appId, ActionType actionType, int questionedUserId);
    }
}