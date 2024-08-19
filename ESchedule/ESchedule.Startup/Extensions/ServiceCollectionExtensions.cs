using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Responses;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Modules;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Modules;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Policy.Requirements;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ESchedule.Startup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddModule<BusinessModule>();
            services.AddModule<DataAccessModule>();

            services.AddAutoMapper(GetAutoMapperConfigs());

            return services;
        }

        public static IServiceCollection ConfigureDbConnection(this IServiceCollection services, ConfigurationManager config)
            => services
                .AddDbContext<TenantEScheduleDbContext>(ServiceLifetime.Transient)
                .AddDbContext<EScheduleDbContext>(
                    opt => opt.UseSqlServer(config.GetConnectionString("SqlServer")!),
                    ServiceLifetime.Transient
                );

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, DispatcherRoleHandler>();
            services.AddSingleton<IAuthorizationHandler, TeacherRoleHandler>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.TeacherOnly, p => p.Requirements.Add(new TeacherRoleRequirement()));
                opt.AddPolicy(Policies.DispatcherOnly, p => p.Requirements.Add(new DispatcherRoleRequirement()));
            });
        }

        public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
            => services
                .AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", cfg =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(jwtSettings.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = key
                    };

                    cfg.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            await context.Response.WriteAsJsonAsync(
                                new ServiceResult<Empty>().Fail("Unauthorized")
                            );
                        }
                    };

                    cfg.Events = new JwtBearerEvents
                    {
                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            await context.Response.WriteAsJsonAsync(
                                new ServiceResult<Empty>().Fail("You don't have permissions for this action")
                            );
                        }
                    };
                });

        private static IServiceCollection AddModule<TModule>(this IServiceCollection services)
            where TModule : IModule, new()
        {
            var module = new TModule();
            module.ConfigureModule(services);

            return services;
        }

        private static Action<IMapperConfigurationExpression> GetAutoMapperConfigs()
            => cfg =>
            {
                cfg.CreateMap<UserCreateModel, UserModel>();
                cfg.CreateMap<UserUpdateModel, UserModel>();
                cfg.CreateMap<UserModel, UserUpdateModel>();

                cfg.CreateMap<GroupCreateModel, GroupModel>();
                cfg.CreateMap<GroupUpdateModel, GroupModel>();

                cfg.CreateMap<LessonCreateModel, LessonModel>();
                cfg.CreateMap<LessonUpdateModel, LessonModel>();

                cfg.CreateMap<TenantUpdateModel, TenantModel>();
                cfg.CreateMap<TenantCreateModel, TenantModel>();
                cfg.CreateMap<TenantSettingsUpdateModel, TenantSettingsModel>();
                cfg.CreateMap<TenantSettingsCreateModel, TenantSettingsModel>();
                cfg.CreateMap<RequestTenantAccessCreateModel, RequestTenantAccessModel>();

                cfg.CreateMap<ScheduleUpdateModel, ScheduleModel>();
                cfg.CreateMap<ScheduleCreateModel, ScheduleModel>();
                cfg.CreateMap<ScheduleModel, ScheduleItemResponse>()
                    .ForPath(x => x.Teacher.Name, o => o.MapFrom(x => x.Teacher.Name))
                    .ForPath(x => x.Teacher.LastName, o => o.MapFrom(x => x.Teacher.LastName))
                    .ForPath(x => x.Teacher.FatherName, o => o.MapFrom(x => x.Teacher.FatherName))
                    .ForMember(x => x.GroupName, o => o.MapFrom(x => x.StudyGroup.Title))
                    .ForMember(x => x.LessonName, o => o.MapFrom(x => x.Lesson.Title));

                cfg.CreateMap<RuleInputModel, RuleModel>();

                cfg.CreateMap<TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel>();
                cfg.CreateMap<TeachersLessonsCreateModel, TeachersLessonsModel>();
                cfg.CreateMap<GroupsLessonsCreateModel, GroupsLessonsModel>();
            };
    }
}
