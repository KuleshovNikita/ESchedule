using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Auth;
using ESchedule.Business.Email;
using ESchedule.Business.Mappers;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Users;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;
using System.Security.Authentication;
using static Moq.It;

namespace ESchedule.Business.UnitTests.Auth;

public class AuthServiceTests : TestBase<AuthService>
{
    private Mock<IEmailService> _mockEmailService;
    private Mock<IUserService> _mockUserService;
    private Mock<IAuthRepository> _mockAuthRepository;
    private Mock<IPasswordHasher> _mockPasswordHasher;
    private Mock<IRepository<UserModel>> _mockUserRepository;
    private Mock<IMainMapper> _mockMapper;
    private IConfiguration _configuration;

    public AuthServiceTests()
    {
        _mockEmailService = new Mock<IEmailService>();
        _mockUserService = new Mock<IUserService>();
        _mockAuthRepository = new Mock<IAuthRepository>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockUserRepository = new Mock<IRepository<UserModel>>();
        _mockMapper = new Mock<IMainMapper>();

        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Jwt:Issuer", "issuer" },
            { "Jwt:Secret", "1234567890123456123456789123456789" },
            { "Jwt:Audience", "audience" },
            { "Jwt:ExpiresInMinutes", "0" },
        };

        _configuration = new ConfigurationBuilder()
                        .AddInMemoryCollection(inMemorySettings)
                        .Build();

        Sut = GetNewSut();
    }

    [Fact]
    public async Task ConfirmEmail_Throws_WhenEmailConfirmed()
    {
        _mockAuthRepository
            .Setup(x => x.FirstOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { IsEmailConfirmed = true });

        var action = async () => await Sut.ConfirmEmail("test-key");

        await action.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task ConfirmEmail_ReturnsUserId_OnSuccessConfirmation()
    {
        var userId = Guid.NewGuid();
        var userModel = new UserModel { IsEmailConfirmed = false, Id = userId };
        var userUpdateModel = new UserUpdateModel { Id = userId };
        _mockAuthRepository
            .Setup(x => x.FirstOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userModel);
        _mockMapper
            .Setup(x => x.Map<UserUpdateModel>(userModel))
            .Returns(userUpdateModel);
        _mockMapper
            .Setup(x => x.MapOnlyUpdatedProperties(IsAny<UserUpdateModel>(), IsAny<UserModel>()))
            .Returns(new UserModel { IsEmailConfirmed = true, Id = userUpdateModel.Id });
        _mockUserRepository
            .Setup(x => x.Any(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(true);

        var id = await Sut.ConfirmEmail("test-key");

        id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Login_Throws_IfArgumentIsNull()
    {
        var action = async () => await Sut.Login(null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Login_Throws_IfLoginIsNotEmailFormat()
    {
        var action = async () => await Sut.Login(new AuthModel { Login = "invalid_login" });

        await action.Should().ThrowAsync<FormatException>();
    }

    [Fact]
    public async Task Login_Throws_IfEmailNotConfirmed()
    {
        _mockAuthRepository
            .Setup(x => x.FirstOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { IsEmailConfirmed = false });

        var action = async () => await Sut.Login(new AuthModel { Login = "admin@admin.com" });

        await action.Should().ThrowAsync<AuthenticationException>();
    }

    [Fact]
    public async Task Login_Throws_IfPasswordIncorrect()
    {
        _mockAuthRepository
            .Setup(x => x.FirstOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { IsEmailConfirmed = true });
        _mockPasswordHasher
            .Setup(x => x.ComparePasswords(IsAny<string>(), IsAny<string>()))
            .Returns(false);

        var action = async () => await Sut.Login(new AuthModel { Login = "admin@admin.com" });

        await action.Should().ThrowAsync<AuthenticationException>();
    }

    [Fact]
    public async Task Login_ReturnsToken_ForValidCredentials()
    {
        var userModel = new UserModel
        {
            IsEmailConfirmed = true,
            Name = "name",
            LastName = "lastName",
            Login = "admin@admin.com",
            Role = Role.Teacher,
            Id = Guid.NewGuid(),
        };
        _mockAuthRepository
            .Setup(x => x.FirstOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userModel);
        _mockPasswordHasher
            .Setup(x => x.ComparePasswords(IsAny<string>(), IsAny<string>()))
            .Returns(true);

        var token = await Sut.Login(new AuthModel { Login = "admin@admin.com", Password = "password" });

        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Register_Throws_IfArgumentIsNull()
    {
        var action = async () => await Sut.Register(null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Register_Throws_IfLoginAlreadyRegistered()
    {
        var userCreateModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };

        _mockAuthRepository
            .Setup(x => x.Any(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(true);

        var action = async () => await Sut.Register(userCreateModel);

        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Register_Throws_IfLoginIsNotEmailFormat()
    {
        var mappedUserModel = new UserModel
        {
            Login = "invalid_login"
        };

        var action = async () => await Sut.Register(new());

        await action.Should().ThrowAsync<FormatException>();
    }

    [Fact]
    public async Task Register_CallsPasswordHasher()
    {
        var createModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };
        var mappedUserModel = new UserModel
        {
            Login = "admin@admin.com"
        };
        _mockMapper
            .Setup(x => x.Map<UserModel>(IsAny<UserCreateModel>()))
            .Returns(mappedUserModel);

        await Sut.Register(createModel);

        _mockPasswordHasher.Verify(x => x.HashPassword(IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Register_AppliesPasswordToUserModel()
    {
        var hashedPassword = "hashed_password";
        var createModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };
        var mappedUserModel = new UserModel
        {
            Login = "admin@admin.com",
            Password = string.Empty
        };
        _mockMapper
            .Setup(x => x.Map<UserModel>(IsAny<UserCreateModel>()))
            .Returns(mappedUserModel);
        _mockPasswordHasher
            .Setup(x => x.HashPassword(IsAny<string>()))
            .Returns(hashedPassword);

        await Sut.Register(createModel);

        mappedUserModel.Password.Should().Be(hashedPassword);
    }

    [Fact]
    public async Task Register_GeneratesIdForUser()
    {
        var createModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };
        var mappedUserModel = new UserModel
        {
            Login = "admin@admin.com",
            Id = Guid.Empty
        };
        _mockMapper
            .Setup(x => x.Map<UserModel>(IsAny<UserCreateModel>()))
            .Returns(mappedUserModel);

        await Sut.Register(createModel);

        mappedUserModel.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Register_SavesUserToStorage()
    {
        var createModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };
        var mappedUserModel = new UserModel
        {
            Login = "admin@admin.com"
        };
        _mockMapper
            .Setup(x => x.Map<UserModel>(IsAny<UserCreateModel>()))
            .Returns(mappedUserModel);

        await Sut.Register(createModel);

        _mockUserRepository.Verify(x => x.Insert(mappedUserModel), Times.Once);
    }

    [Fact]
    public async Task Register_SendsConfirmationEmailToUser()
    {
        var createModel = new UserCreateModel
        {
            Login = "admin@admin.com"
        };
        var mappedUserModel = new UserModel
        {
            Login = "admin@admin.com"
        };
        _mockMapper
            .Setup(x => x.Map<UserModel>(IsAny<UserCreateModel>()))
            .Returns(mappedUserModel);

        await Sut.Register(createModel);

        _mockEmailService.Verify(x => x.SendConfirmEmailMessage(mappedUserModel), Times.Once);
    }

    protected override AuthService GetNewSut()
        => new AuthService(
            _mockUserRepository.Object,
            _mockMapper.Object,
            _mockPasswordHasher.Object,
            _mockEmailService.Object,
            _configuration,
            _mockUserService.Object,
            _mockAuthRepository.Object
        );
}