using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Groups;
using ESchedule.DataAccess.Repos.User;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.DataAccess.Modules
{
    public class DataAccessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IRepository<UserModel>, UserRepository>();
            services.AddScoped<IRepository<GroupModel>, GroupRepository>();

            return services;
        }
    }
}
