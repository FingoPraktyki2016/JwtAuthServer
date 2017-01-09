using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IChangeAppUserRole : IAction
    {
        void Invoke(int appId, int user, int role);
    }
}
