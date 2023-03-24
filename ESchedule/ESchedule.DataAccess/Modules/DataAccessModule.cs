using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.User;
using ESchedule.Domain.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.DataAccess.Modules
{
    public class DataAccessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
