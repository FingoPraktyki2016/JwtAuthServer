using LegnicaIT.BusinessLogic.Actions.Base;
using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.BusinessLogic.Configuration.Helpers
{
    public static class RegisterDependecy
    {
        public static void Register(IServiceCollection services)
        {
            var dbDependencyBuilder = new DependencyBuilder<IRepository>();
            dbDependencyBuilder.RegisterRepositories(services);

            var blDependencyBuilder = new DependencyBuilder<IAction>();
            blDependencyBuilder.RegisterRepositories(services);
        }
    }
}