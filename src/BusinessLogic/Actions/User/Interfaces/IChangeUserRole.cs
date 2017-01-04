using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IChangeUserRole :IAction
    {
        void Invoke(int user, int role, int appId);
    }
}
