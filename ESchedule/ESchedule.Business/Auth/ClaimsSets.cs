using ESchedule.Domain;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ESchedule.Business.Auth
{
    public static class ClaimsSets
    {
        public static IReadOnlyCollection<Claim> GetInitialClaims(UserCredentialsModel credentialsModel)
            => new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, credentialsModel.Id.ToString()),
            };

        public static IReadOnlyCollection<Claim> GetClaimsWithEmail(UserCredentialsModel userModel)
            => new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Login)
            };
    }
}
