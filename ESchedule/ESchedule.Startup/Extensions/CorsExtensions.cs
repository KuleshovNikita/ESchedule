using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ESchedule.Startup.Extensions
{
    public static class CorsExtensions
    {
        public static void AllowAnyOriginPolicy(this CorsOptions source)
            => source.AddDefaultPolicy(
                    configurePolicy: p => p.WithOrigins("http://localhost:3000", "https://localhost:3000")
                                           .AllowAnyHeader()
                                           .AllowCredentials()
                                           .AllowAnyMethod()
            );
    }
}
