using ESchedule.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PowerInfrastructure.Integration;

namespace ESchedule.Business.MigrationCommands;

public class DatabaseMigrationCommand(ILogger<DatabaseMigrationCommand> logger, EScheduleDbContext dbContext) : IMigrationCommand
{
    public async Task Execute()
    {
        logger.LogDebug("Starting {commandName} migration command", nameof(DatabaseMigrationCommand));

        try
        {
            logger.LogInformation("Migrating database");

            await dbContext.Database.MigrateAsync();

            logger.LogInformation("Database migrated successfully");
        }
        catch (Exception ex)
        {
            logger.LogError("Failed running database migrations: {message}", ex.Message);
            throw;
        }
    }
}
