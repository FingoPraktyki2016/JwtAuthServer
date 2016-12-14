using Autofac;
using LegnicaIT.BusinessLogic.Repositories;
using LegnicaIT.DataAccess.Context;

namespace JwtAuthServer.Autofac
{
    public class AutofacHelper
    {
        public ContainerBuilder RegisterComponents()
        {
            var autofacBuilder = new ContainerBuilder();
            autofacBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            autofacBuilder.RegisterType<AppRepository>().As<IAppRepository>();
            autofacBuilder.RegisterType<JwtDbContext>().As<IJwtDbContext>();

            return autofacBuilder;
        }
    }
}