
using Microsoft.Extensions.Hosting;

namespace PowerInfrastructure.Extensions;

public static class IHostEnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment environment)
        => !environment.IsProduction() && !environment.IsStaging();
}
