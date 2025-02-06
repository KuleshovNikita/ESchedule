using Microsoft.Extensions.DependencyInjection;

namespace PowerInfrastructure.DependencyInjection;

public interface IDependencyModule
{
    IServiceCollection ConfigureModule(IServiceCollection services);
}
