using Autofac;
using LegnicaIT.BusinessLogic.Repositories;
using LegnicaIT.DataAccess.Context;

namespace JwtAuthServer.Autofac
{
    public static class AutofacBuilder
    {
        public static IContainer RegisterComponents()
        {
            var autofacBuilder = new ContainerBuilder();
            autofacBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            autofacBuilder.RegisterType<AppRepository>().As<IAppRepository>();
            autofacBuilder.RegisterType<JwtDbContext>().As<IJwtDbContext>();
            IContainer _ApplicationContainer = autofacBuilder.Build();

            return _ApplicationContainer;
        }
    }
}