using LegnicaIT.BusinessLogic.Repositories;
using LegnicaIT.BusinessLogic.Repositories.Interfaces;
using LegnicaIT.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.JwtAuthServer.DependencyInjection
{
    public class DependencyBuilder
    {
        public void RegisterRepositories(IServiceCollection services)
        {
            services.AddSingleton<IJwtDbContext, JwtDbContext>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAppRepository, AppRepository>();
        }
    }
}
