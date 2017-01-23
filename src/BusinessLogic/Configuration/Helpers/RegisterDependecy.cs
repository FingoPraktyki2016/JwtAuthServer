using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.BusinessLogic.Configuration.Helpers
{
    public static class RegisterDependecy
    {
        public static void Register(IServiceCollection services)
        {
            var dbDependencyBuilder = new DependencyBuilder<IRepository>();
            dbDependencyBuilder.Register(services);

            var blDependencyBuilder = new DependencyBuilder<IAction>();
            blDependencyBuilder.Register(services);
        }
    }
}