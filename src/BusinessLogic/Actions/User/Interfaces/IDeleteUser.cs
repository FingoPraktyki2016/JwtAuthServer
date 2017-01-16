using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IDeleteUser : IAction
    {
        void Invoke(int id);
    }
}