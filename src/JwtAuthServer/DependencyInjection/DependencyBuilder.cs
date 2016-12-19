using LegnicaIT.BusinessLogic.Repositories;
using LegnicaIT.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.JwtAuthServer.Services
{
    public class DependencyBuilder
    {
        public void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IJwtDbContext, JwtDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppRepository, AppRepository>();
        }
    }
}