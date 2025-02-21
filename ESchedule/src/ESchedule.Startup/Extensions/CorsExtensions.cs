using Microsoft.AspNetCore.Cors.Infrastructure;

namespace ESchedule.Startup.Extensions;

public static class CorsExtensions
{
    internal static void AllowAnyOriginPolicy(this CorsOptions source)
        => source.AddDefaultPolicy(
                configurePolicy: p => p.WithOrigins("*")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod()
        );
}
