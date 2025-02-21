using ESchedule.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ESchedule.Domain.Policy.Requirements;

public class RequirementsBase
{
    public Task VerifyRole(AuthorizationHandlerContext context, IAuthorizationRequirement requirement, Role role)
    {
        var httpContext = (HttpContext)context.Resource!;

        if (httpContext.User.Claims.Any(x => x.Type == JwtRegisteredClaimNames.Typ && x.Value == role.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
