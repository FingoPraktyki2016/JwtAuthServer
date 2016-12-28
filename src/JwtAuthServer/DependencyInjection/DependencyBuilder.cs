using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LegnicaIT.JwtAuthServer.DependencyInjection
{
    public class DependencyBuilder<T>
    {
        //Registers interfaces that inherit IRepository
        public void RegisterRepositories(IServiceCollection services)
        {
            //get all assemblies
            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(assemblyType =>
            {
                //find classes in assemblies
                Assembly.Load(assemblyType).GetTypes().Where(assemblyClass => assemblyClass.GetTypeInfo().IsClass).ToList().ForEach(implementation =>
                 {
                     //if class's interface inherits IRepository register it
                     var interfacee = implementation.GetInterfaces().Where(Iimplementation => Iimplementation.GetInterfaces().Contains(typeof(T))).FirstOrDefault();
                     if (interfacee != null)
                     {
                         services.AddScoped(interfacee, implementation);
                         Debug.WriteLine($"Registered interface {interfacee.Name} to {implementation.Name}");
                     }
                 });
            });
        }
    }
}
