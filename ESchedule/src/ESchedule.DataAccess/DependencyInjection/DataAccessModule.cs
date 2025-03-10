﻿using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.DataAccess.Repos.Groups;
using ESchedule.DataAccess.Repos.Lessons;
using ESchedule.DataAccess.Repos.ManyToMany;
using ESchedule.DataAccess.Repos.Schedule;
using ESchedule.DataAccess.Repos.Tenant;
using ESchedule.DataAccess.Repos.User;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using PowerInfrastructure.DependencyInjection;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.DataAccess.DependencyInjection;

public class DataAccessModule : IDependencyModule
{
    public IServiceCollection ConfigureModule(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRepository<UserModel>, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IRepository<GroupModel>, GroupRepository>();
        services.AddScoped<IRepository<LessonModel>, LessonRepository>();
        services.AddScoped<IRepository<TenantSettingsModel>, TenantSettingsRepository>();
        services.AddScoped<IRepository<ScheduleModel>, ScheduleRepository>();
        services.AddScoped<IRepository<TenantModel>, TenantRepository>();
        services.AddScoped<IRepository<TeachersLessonsModel>, TeachersLessonsRepository>();
        services.AddScoped<IRepository<TeachersGroupsLessonsModel>, TeachersGroupsLessonsRepository>();
        services.AddScoped<IRepository<GroupsLessonsModel>, GroupsLessonsRepository>();
        services.AddScoped<IRepository<RuleModel>, RulesRepository>();
        services.AddScoped<IRepository<RequestTenantAccessModel>, RequestTenantAccessRepository>();

        services.AddScoped<ITenantContextProvider, TenantContextProvider>();

        return services;
    }
}
