using ESchedule.Business.Hashing;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;

namespace ESchedule.Business.UnitTests.Hashing;

public class PasswordHasherTests : TestBase<PasswordHasher>
{
    public PasswordHasherTests()
    {
        Sut = GetNewSut();
    }

    [Fact]
    public void HashPassword_ReturnsHashedValue()
    {
        var password = "my-password";

        var hashedPassword = Sut.HashPassword(password);

        hashedPassword.Should().NotBeEquivalentTo(password);
    }

    [Fact]
    public void HashPassword_ReturnsDifferentHashesForSameValue()
    {
        var password = "my-password";
        var sut = GetNewSut();

        var hashedPassword1 = Sut.HashPassword(password);
        var hashedPassword2 = Sut.HashPassword(password);

        hashedPassword1.Should().NotBeEquivalentTo(hashedPassword2);
    }

    [Fact]
    public void ComparePasswords_ReturnsTrueIfPasswordHashesMatch()
    {
        var password = "my-password";
        var hashedPassword = Sut.HashPassword(password);

        var passwordsEqual = Sut.ComparePasswords(password, hashedPassword);

        passwordsEqual.Should().BeTrue();
    }

    [Fact]
    public void ComparePasswords_ReturnsFalseIfPasswordHashesDoNotMatch()
    {
        var password = "my-password";
        var hashedPassword = Sut.HashPassword("wrong-password");

        var passwordsEqual = Sut.ComparePasswords(password, hashedPassword);

        passwordsEqual.Should().BeFalse();
    }

    protected override PasswordHasher GetNewSut()
        => new();
}
