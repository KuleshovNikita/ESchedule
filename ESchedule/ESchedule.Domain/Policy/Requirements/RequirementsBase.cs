using ESchedule.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESchedule.Domain.Policy.Requirements
{
    public class RequirementsBase
    {
        public Task VerifyRole(AuthorizationHandlerContext context, IAuthorizationRequirement requirement, Role role)
        {
            var authFilterContext = (AuthorizationFilterContext)context.Resource!;
            var httpContext = authFilterContext.HttpContext;

            if (httpContext.User.Claims.Any(x => x.Type == role.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
