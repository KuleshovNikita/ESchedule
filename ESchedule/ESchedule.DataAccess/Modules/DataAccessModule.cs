using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.User.Pupil;
using ESchedule.DataAccess.Repos.User.Teacher;
using ESchedule.Domain.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.DataAccess.Modules
{
    public class DataAccessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IPupilRepository, PupilRepository>();

            return services;
        }
    }
}
