using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetAppUserRole : IAction
    {
        string Invoke(int appId, int user);
    }
}
