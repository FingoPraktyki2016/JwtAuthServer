using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.Role.Interfaces
{
    public interface IDeleteRole : IAction
    {
        void Invoke(int roleId);
    }
}