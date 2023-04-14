using ESchedule.Domain.Users;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ESchedule.Business.Auth
{
    public static class ClaimsSets
    {
        public static IReadOnlyCollection<Claim> GetClaimsWithEmail(UserModel userModel)
            => new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{userModel.Name} {userModel.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Login)
            };
    }
}
