using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Groups;
using ESchedule.DataAccess.Repos.Lessons;
using ESchedule.DataAccess.Repos.ManyToMany;
using ESchedule.DataAccess.Repos.Schedule;
using ESchedule.DataAccess.Repos.Tenant;
using ESchedule.DataAccess.Repos.User;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
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
            services.AddScoped<IRepository<LessonModel>, LessonRepository>();
            services.AddScoped<IRepository<TenantSettingsModel>, TenantSettingsRepository>();
            services.AddScoped<IRepository<ScheduleModel>, ScheduleRepository>();
            services.AddScoped<IRepository<TenantModel>, TenantRepository>();
            services.AddScoped<IRepository<TeachersLessonsModel>, TeachersLessonsRepository>();
            services.AddScoped<IRepository<TeachersGroupsLessonsModel>, TeachersGroupsLessonsRepository>();
            services.AddScoped<IRepository<GroupsLessonsModel>, GroupsLessonsRepository>();
            services.AddScoped<IRepository<RuleModel>, RulesRepository>();

            services.AddScoped<ITenantContextProvider, TenantContextProvider>();

            return services;
        }
    }
}
