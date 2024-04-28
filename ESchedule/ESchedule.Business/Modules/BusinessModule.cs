using ESchedule.Business.Auth;
using ESchedule.Business.Email;
using ESchedule.Business.GroupLessons;
using ESchedule.Business.Hashing;
using ESchedule.Business.Lessons;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Business.TeachersLessons;
using ESchedule.Business.Tenant;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Business.Modules
{
    public class BusinessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IScheduleBuilder, ScheduleBuilder>();

            services.AddScoped<ITenantSettingsService, TenantSettingsService>();
            services.AddScoped<ITenantService, TenantService>();

            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAttendanceService, AttendanceService>();

            services.AddScoped<IBaseService<GroupsLessonsModel>, GroupLessonsService>();
            services.AddScoped<IBaseService<TeachersLessonsModel>, TeachersLessonsService>();
            services.AddScoped<IBaseService<TeachersGroupsLessonsModel>, TeachersGroupsLessonsService>();

            return services;
        }
    }
}
