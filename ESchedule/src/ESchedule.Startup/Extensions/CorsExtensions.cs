using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ESchedule.Startup.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddCors(AllowAnyOriginPolicy);
        }

        return services;
    }

    private static void AllowAnyOriginPolicy(this CorsOptions source)
        => source.AddDefaultPolicy(
                configurePolicy: p => p.WithOrigins("*")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod()
        );
}
