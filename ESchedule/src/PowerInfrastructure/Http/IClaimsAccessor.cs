namespace PowerInfrastructure.Http;

public interface IClaimsAccessor
{
    string? GetClaimValue(string claimType);

    string GetRequiredClaimValue(string claimType);
}
