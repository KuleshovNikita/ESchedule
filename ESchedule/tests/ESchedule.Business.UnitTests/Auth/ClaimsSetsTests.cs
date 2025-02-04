using ESchedule.Business.Auth;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Users;
using FluentAssertions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ESchedule.Business.UnitTests.Auth;

public class ClaimSetsTests
{
    private UserModel _userModel;

    public ClaimSetsTests()
    {
        _userModel = new UserModel
        {
            IsEmailConfirmed = true,
            Name = "name",
            LastName = "lastName",
            Login = "admin@admin.com",
            Role = Role.Teacher,
            Id = Guid.NewGuid(),
        };
    }

    [Fact]
    public void GetClaims_ReturnsClaimsSet()
    {
        var claims = ClaimsSets.GetClaims(_userModel);

        claims.Should().NotBeEmpty();
    }

    [Fact]
    public void GetClaims_ReturnsClaimsSetWithoutTenant_IfUserHasNoOne()
    {
        var claims = ClaimsSets.GetClaims(_userModel);

        claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName).Should().BeNull();
    }

    [Fact]
    public void GetClaims_ReturnsClaimsSetWithTenant_IfUserHasOne()
    {
        _userModel.TenantId = Guid.NewGuid();
        var claims = ClaimsSets.GetClaims(_userModel);

        claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName).Should().NotBeNull();
    }
}