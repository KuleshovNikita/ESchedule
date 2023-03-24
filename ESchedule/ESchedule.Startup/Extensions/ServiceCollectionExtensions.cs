using ESchedule.Business.Modules;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Modules;
using ESchedule.Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Startup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddModule<BusinessModule>();
            services.AddModule<DataAccessModule>();

            return services;
        }

        public static IServiceCollection ConfigureDbConnection(this IServiceCollection services, ConfigurationManager config)
            => services
                .AddDbContext<EScheduleDbContext>(
                    opt => opt.UseSqlServer(config.GetConnectionString("SqlServer"))
                );

        private static IServiceCollection AddModule<TModule>(this IServiceCollection services)
            where TModule : IModule, new()
        {
            var module = new TModule();
            module.ConfigureModule(services);

            return services;
        }
    }
}
