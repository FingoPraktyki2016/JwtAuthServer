using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.Role.Interfaces
{
    public interface IAddNewRole : IAction
    {
        void Invoke(string roleName);
    }
}