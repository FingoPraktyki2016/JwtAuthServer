using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IConfirmUserEmail : IAction
    {
        void Invoke(int userId);
    }
}