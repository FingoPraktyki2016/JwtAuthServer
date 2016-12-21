using LegnicaIT.DataAccess.Context;
using LegnicaIT.DataAccess.Repositories.Implementations;
using LegnicaIT.DataAccess.Repositories.Interfaces;
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
