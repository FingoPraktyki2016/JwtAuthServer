using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.App.Interfaces
{
    public interface IDeleteApp : IAction
    {
        void Invoke(int id);
    }
}