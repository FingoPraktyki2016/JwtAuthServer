using LegnicaIT.BusinessLogic.Actions.Base;

namespace LegnicaIT.BusinessLogic.Configuration.Interfaces
{
    public interface IMigrationHelper : IAction
    {
        void Migrate();
    }
}