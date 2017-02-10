using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IConfirmUserEmail : IAction
    {
        bool Invoke(int userId);
    }
}