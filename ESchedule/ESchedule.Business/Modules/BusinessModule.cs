using ESchedule.Business.Auth;
using ESchedule.Business.Email;
using ESchedule.Business.Hashing;
using ESchedule.Business.Users.Teacher;
using ESchedule.Domain.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Business.Modules
{
    public class BusinessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
