using ESchedule.Business.Mappers.Profiles;
using ESchedule.Business.DependencyInjection;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Modules;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Policy.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace ESchedule.Startup.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDevelopmentServices(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if(environment.IsDevelopment())
        {
            services.AddSwagger();
            services.AddCors(x => x.AllowAnyOriginPolicy());
        }

        return services;
    }

    public static IServiceCollection RegisterDependencyModules(this IServiceCollection services)
    {
        services.AddModule<BusinessModule>();
        services.AddModule<DataAccessModule>();

        return services;
    }

    public static IServiceCollection AddAutoMappers(this IServiceCollection services)
    {
        return services.AddAutoMapper([Assembly.GetAssembly(typeof(UserProfile))]);
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, ConfigurationManager config)
        => services
            .AddDbContext<TenantEScheduleDbContext>(ServiceLifetime.Transient)
            .AddDbContext<EScheduleDbContext>(
                opt => opt.UseSqlServer(config.GetConnectionString("SqlServer")!),
                ServiceLifetime.Transient
            );

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, DispatcherRoleHandler>();
        services.AddSingleton<IAuthorizationHandler, TeacherRoleHandler>();

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(Policies.TeacherOnly, p => p.Requirements.Add(new TeacherRoleRequirement()));
            opt.AddPolicy(Policies.DispatcherOnly, p => p.Requirements.Add(new DispatcherRoleRequirement()));
        });

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetRequiredSection("Jwt").Get<JwtSettings>()!;

        services
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
                            new ProblemDetails
                            {
                                Status = 401,
                                Detail = "Not authenticated"
                            }
                        );
                    }
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsJsonAsync(
                            new ProblemDetails
                            {
                                Status = 403,
                                Detail = "You don't have permissions for this action"
                            }
                        );
                    }
                };
            });

        return services;
    }

    private static IServiceCollection AddModule<TModule>(this IServiceCollection services)
        where TModule : IModule, new()
    {
        var module = new TModule();
        module.ConfigureModule(services);

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(cfg =>
        {
            cfg.ResolveConflictingActions(api => api.First());
            cfg.AddSecurityDefinition("Bearer", new()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Name = "Authorization",
                In = ParameterLocation.Header
            });
            cfg.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
}