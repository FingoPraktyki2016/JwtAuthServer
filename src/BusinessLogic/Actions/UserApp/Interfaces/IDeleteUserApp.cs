using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.UserApp.Interfaces
{
    public interface IDeleteUserApp : IAction
    {
        void Invoke(int userAppId);
    }
}