using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.Integration;

namespace PowerInfrastructure.Extensions;

public static class IHostExtensions
{
    public static async Task<bool> Migrate(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var migrationCommands = scope.ServiceProvider.GetServices<IMigrationCommand>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();

        try
        {
            foreach (var command in migrationCommands)
            {
                logger.LogTrace("Running migration command: {commandName}", command.GetType().Name);

                await command.Execute();

                logger.LogTrace("Migration command completed successfully");
            }

            return true;
        }
        catch (Exception) 
        {
            return false;
        }
    }
}
