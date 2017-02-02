using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface ILockUser : IAction
    {
        void Invoke(int id);
    }
}
