using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Enums;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces
{
    public interface IAddNewUserApp : IAction
    {
        void Invoke(int userId, int appId, UserRole role);
    }
}