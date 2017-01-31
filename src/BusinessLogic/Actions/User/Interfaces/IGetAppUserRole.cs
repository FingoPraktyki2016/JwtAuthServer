using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.User.Interfaces
{
    public interface IGetAppUserRole : IAction
    {
        UserRole Invoke(int appId, int userId);
    }
}
