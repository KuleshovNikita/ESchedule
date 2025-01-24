using ESchedule.Business.Mappers.Profiles;
using ESchedule.Business.Modules;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Modules;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Policy.Requirements;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace ESchedule.Startup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddModule<BusinessModule>();
            services.AddModule<DataAccessModule>();

            services.AddAutoMapper([Assembly.GetAssembly(typeof(UserProfile))]);

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
    }
}
