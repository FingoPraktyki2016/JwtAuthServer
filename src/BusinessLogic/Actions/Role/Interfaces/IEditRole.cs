using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models.Role;

namespace LegnicaIT.BusinessLogic.Actions.Role.Interfaces
{
    public interface IEditRole : IAction
    {
        void Invoke(RoleModel app);
    }
}