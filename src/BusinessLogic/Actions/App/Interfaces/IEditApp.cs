using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.BusinessLogic.Models;

namespace LegnicaIT.BusinessLogic.Actions.App.Interfaces
{
    public interface IEditApp : IAction
    {
        void Invoke(AppModel app);
    }
}
