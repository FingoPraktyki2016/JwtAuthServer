using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.Role;

namespace LegnicaIT.BusinessLogic.Actions.Role.Interfaces
{
    public interface IAddNewRole : IAction
    {
        void Invoke(RoleModel role);
    }
}
