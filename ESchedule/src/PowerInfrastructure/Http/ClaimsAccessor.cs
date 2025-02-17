using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PowerInfrastructure.Http;

public class ClaimsAccessor(IHttpContextAccessor httpContextAccessor) : IClaimsAccessor
{
    public string? GetClaimValue(string claimType)
        => httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

    public string GetRequiredClaimValue(string claimType)
        => GetClaimValue(claimType)!;

}
