using ESchedule.Business.Tenant;
using ESchedule.Domain.Tenant;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ESchedule.Api.Filters
{
    public class TenantContextFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public TenantContextFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var tenantClaim = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Surname);

            if (tenantClaim == null)
            {
                return;
            }
            
            var tenantId = Guid.Parse(tenantClaim.Value);
            var tenantContext = new TenantContext(tenantId);

            var contextProvider = _serviceProvider.GetRequiredService<ITenantContextProvider>();
            contextProvider.UseContext(tenantContext);
        }
    }
}
