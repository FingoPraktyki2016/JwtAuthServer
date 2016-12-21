using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LegnicaIT.JwtAuthServer.DependencyInjection
{
    public class RepositoryModel
    {
        private Type imp;
        private Type inter;
    }

    public class DependencyBuilder<T>
    {
        //Registers interfaces that inherit IRepository
        public void RegisterRepositories(IServiceCollection services)
        {
            var listOfAssembliesToIjnect =
                    (from projectAssembly in Assembly.GetEntryAssembly().GetReferencedAssemblies()
                     from assemblyType in Assembly.Load(projectAssembly).GetTypes()
                     from assemblyTypeInterfaces in assemblyType.GetInterfaces()
                     where assemblyTypeInterfaces == typeof(T)
                     select assemblyType).ToList();

            //foreach (var implementation in listOfAssembliesToIjnect)
            //{
            //    Debug.WriteLine($"Registered interface {implementation.Name}");
            //}

            foreach (var implementation in listOfAssembliesToIjnect)
            {
                foreach (var interfacee in listOfAssembliesToIjnect)
                {
                    if (implementation.GetInterfaces().Contains(interfacee))
                    {
                        services.AddScoped(interfacee, implementation);
                        Debug.WriteLine($"Registered interface {interfacee.Name} to {implementation.Name}");
                        break;
                    }
                }
            }
        }
    }
}
