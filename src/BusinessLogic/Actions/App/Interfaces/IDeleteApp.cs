using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Actions.App.Interfaces
{
    public interface IDeleteApp : IAction
    {
        bool Invoke(int id);
    }
}