using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetUserId : IAction
    {
        int Invoke(string email);
    }
}
