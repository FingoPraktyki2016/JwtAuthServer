using LegnicaIT.BusinessLogic.Repositories;
using LegnicaIT.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.JwtAuthServer.Services
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