using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface ICheckUserExist : IAction
    {
        bool Invoke(string email, string password);
    }
}