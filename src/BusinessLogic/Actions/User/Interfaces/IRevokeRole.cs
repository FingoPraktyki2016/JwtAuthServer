using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IRevokeRole : IAction
    {
        void Invoke(int appId, int user);
    }
}
