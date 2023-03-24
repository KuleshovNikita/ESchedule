using ESchedule.Domain.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace ESchedule.Business.Modules
{
    public class BusinessModule : IModule
    {
        public IServiceCollection ConfigureModule(IServiceCollection services)
        {
            return services;
        }
    }
}
