using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ESchedule.Domain.Tenant
{
    public class TenantContextProvider : ITenantContextProvider
    {
        public TenantContextProvider(IHttpContextAccessor contextAccessor)
        {
            var tenantClaim = contextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Surname);

            if (tenantClaim == null)
            {
                return;
            }

            var tenantId = Guid.Parse(tenantClaim.Value);
            var tenantContext = new TenantContext(tenantId);

            Current = tenantContext;
        }

        public TenantContext Current { get; private set; }
    }
}
