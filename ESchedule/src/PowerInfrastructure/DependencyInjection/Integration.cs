using Microsoft.Extensions.DependencyInjection;
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
    public static IServiceCollection AddMigrationCommands(this IServiceCollection services, Type type)
    {
        var assembly = Assembly.GetCallingAssembly();

        return services.Scan(x =>
            x.FromAssemblies(assembly)
                .AddClasses(x => x.AssignableTo<IMigrationCommand>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }
}
