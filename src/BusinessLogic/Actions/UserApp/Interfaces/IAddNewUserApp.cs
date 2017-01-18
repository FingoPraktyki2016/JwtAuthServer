using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces
{
    public interface IAddNewUserApp : IAction
    {
        void Invoke(int userId, int appId);
    }
}