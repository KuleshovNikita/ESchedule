using Microsoft.Extensions.DependencyInjection;
using PowerInfrastructure.Http;

namespace PowerInfrastructure.DependencyInjection.Extensions;

public static class HttpExtensions
{
    /// <summary>
    /// Registers <see cref="IClaimsAccessor"/> service in the DI container. 
    /// <see cref="IHttpContextAccessor"/> dependency should be registered to work correctly
    /// </summary>
    /// <param name="services"></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddClaimsAccessor(this IServiceCollection services)
        => services.AddScoped<IClaimsAccessor, ClaimsAccessor>();
}
