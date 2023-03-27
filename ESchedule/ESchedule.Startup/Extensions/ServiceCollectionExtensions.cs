using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Modules;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Modules;
using ESchedule.Domain.Auth;
using ESchedule.Domain.Modules;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authentication;
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
                .AddDbContext<EScheduleDbContext>(
                    opt => opt.UseSqlServer(config.GetConnectionString("SqlServer"))
                );

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
                cfg.CreateMap<UserRequestModel, UserModel>();
                cfg.CreateMap<UserUpdateRequestModel, UserModel>();
                cfg.CreateMap<UserModel, UserUpdateRequestModel>();
            };
    }
}
