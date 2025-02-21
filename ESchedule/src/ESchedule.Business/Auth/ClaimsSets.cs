using ESchedule.Domain.Users;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ESchedule.Business.Auth;

public static class ClaimsSets
{
    public static IReadOnlyCollection<Claim> GetClaims(UserModel userModel)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, userModel.Role.ToString()),
            new Claim(ClaimTypes.Name, $"{userModel.Name} {userModel.LastName}"),
            new Claim(JwtRegisteredClaimNames.Email, userModel.Login),
        };

        if(userModel.TenantId != null)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, userModel.TenantId.ToString()!));
        }

        return claims;
    }
}
