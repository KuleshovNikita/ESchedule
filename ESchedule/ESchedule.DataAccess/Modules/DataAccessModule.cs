using ESchedule.DataAccess.Enums;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.User;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace ESchedule.DataAccess.Modules
{
    public class DataAccessModule : IModule
    {
        public delegate IUserRepository<T> UserRepositoryResolver<T>(UserRepositoryType serviceType) where T : class;

        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository<TeacherModel>, TeacherRepository>();
            services.AddScoped<IUserRepository<PupilModel>, PupilRepository>();

            services.AddTransient<UserRepositoryResolver>()

            return services;
        }
    }
}
