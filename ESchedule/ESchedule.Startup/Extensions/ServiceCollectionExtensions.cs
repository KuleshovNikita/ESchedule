using ESchedule.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Startup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbConnection(this IServiceCollection services, ConfigurationManager config)
            => services
                .AddDbContext<EScheduleDbContext>(
                    opt => opt.UseSqlServer(config.GetConnectionString("SqlServer"))
                );
    }
}
