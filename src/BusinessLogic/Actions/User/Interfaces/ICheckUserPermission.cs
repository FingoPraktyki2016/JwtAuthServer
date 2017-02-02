using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface ICheckUserPermission : IAction
    {
        bool Invoke(int requestorId, int appId, int questionedUserId);
    }
}