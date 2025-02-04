using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using PowerInfrastructure.Integration;
using System.Reflection;

namespace PowerInfrastructure.DependencyInjection;

public static class Integration
{
    /// <summary>
    /// Finds all classes that implement <see cref="IMigrationCommand"/> interface in the calling assembly 
    /// and registers them in the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <returns>
    /// <see cref="IServiceCollection"/> so that other calls can be chained
    /// </returns>
    public static IServiceCollection AddMigrationCommands(this IServiceCollection services)
        => services.Scan(x =>
            x.FromDependencyContext(DependencyContext.Load(Assembly.GetCallingAssembly())!)
                .AddClasses(x => x.AssignableTo<IMigrationCommand>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
}
