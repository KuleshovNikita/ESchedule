using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Domain.Modules
{
    public interface IModule
    {
        IServiceCollection ConfigureModule(IServiceCollection services);
    }
}
