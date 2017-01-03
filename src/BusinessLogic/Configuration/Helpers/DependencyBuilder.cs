using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LegnicaIT.BusinessLogic.Configuration.Helpers
{
    internal class DependencyBuilder<T>
    {
        public void RegisterRepositories(IServiceCollection services)
        {
            //get all assemblies
            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(assemblyType =>
            {
                //find classes in assemblies
                Assembly.Load(assemblyType).GetTypes().Where(assemblyClass => assemblyClass.GetTypeInfo().IsClass).ToList().ForEach(implementation =>
                {
                    //if class's interface inherits IRepository register it
                    var interfacee = implementation.GetInterfaces().FirstOrDefault(Iimplementation => Iimplementation.GetInterfaces().Contains(typeof(T)));
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