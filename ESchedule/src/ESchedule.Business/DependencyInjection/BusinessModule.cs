using ESchedule.Business.Auth;
using ESchedule.Business.Email;
using ESchedule.Business.Email.Client;
using ESchedule.Business.GroupsLessons;
using ESchedule.Business.Hashing;
using ESchedule.Business.Lessons;
using ESchedule.Business.MigrationCommands;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Business.ScheduleRules;
using ESchedule.Business.TeachersGroupsLessons;
using ESchedule.Business.TeachersLessons;
using ESchedule.Business.Tenant;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.Extensions.DependencyInjection;
using PowerInfrastructure.AutoMapper;
using PowerInfrastructure.DependencyInjection;
using PowerInfrastructure.DependencyInjection.Extensions;

namespace ESchedule.Business.DependencyInjection;

public class BusinessModule : IDependencyModule
{
    public IServiceCollection ConfigureModule(IServiceCollection services)
    {
        services.AddMigrationCommands(typeof(DatabaseMigrationCommand));

        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailMessageClient, EmailMessageClient>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IMainMapper, MainMapper>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IRuleService, RuleService>();
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