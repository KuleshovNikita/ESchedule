using ESchedule.Api.Models.Updates;
using ESchedule.Business.Mappers;
using ESchedule.Business.Users;
using ESchedule.Core.Interfaces;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using static Moq.It;

namespace ESchedule.Business.UnitTests.User;

public class UserServiceTests : TestBase<UserService>
{
    private readonly Mock<IRepository<UserModel>> _mockRepository;
    private readonly Mock<IMainMapper> _mockMapper;
    private readonly Mock<IPasswordHasher> _mockPasswordHasher;
    private readonly Mock<ITenantContextProvider> _mockTenantContextProvider;
    private readonly Mock<IAuthRepository> _mockAuthRepository;

    public UserServiceTests()
    {
        _mockRepository = new Mock<IRepository<UserModel>>();
        _mockMapper = new Mock<IMainMapper>();
        _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockTenantContextProvider = new Mock<ITenantContextProvider>();
        _mockAuthRepository = new Mock<IAuthRepository>();

        Sut = GetNewSut();
    }

    [Fact]
    public async Task SignUserToTenant_Throws_IfNoUserFound()
    {
        UserModel returnedData = null!;
        _mockAuthRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(returnedData);

        var action = async () => await Sut.SignUserToTenant(It.IsAny<Guid>(), It.IsAny<Guid>());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task SignUserToTenant_AttachesUserToCorrectTenant()
    {
        var tenantId = Guid.NewGuid();
        var user = new UserModel() { TenantId = Guid.Empty };
        _mockAuthRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(user);

        await Sut.SignUserToTenant(It.IsAny<Guid>(), tenantId);

        user.TenantId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task SignUserToTenant_SavesChangesToStorage()
    {
        _mockAuthRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel());

        await Sut.SignUserToTenant(It.IsAny<Guid>(), It.IsAny<Guid>());

        _mockAuthRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateUser_Throws_IfUserDoesNotExist()
    {
        UserModel returnedUser = null!;
        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(returnedUser);

        var action = async () => await Sut.UpdateUser(new UserUpdateModel());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Theory]
    [InlineData("password", 1)]
    [InlineData(null, 0)]
    public async Task UpdateUser_CallsPasswordHasher_OnlyWhenPasswordChanged(string? password, int expectedPasswordHasherCallsCount)
    {
        _mockRepository
            .Setup(x => x.SingleOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel());
        _mockMapper
            .Setup(x => x.MapOnlyUpdatedProperties(IsAny<IsAnyType>(), IsAny<UserModel>()))
            .Returns(new UserModel { Password = password! });

        await Sut.UpdateUser(new UserUpdateModel { Password = password! });

        _mockPasswordHasher.Verify(x => x.HashPassword(IsAny<string>()), Times.Exactly(expectedPasswordHasherCallsCount));
    }

    [Theory]
    [InlineData("password")]
    [InlineData(null)]
    public async Task UpdateUser_UpdatesDataInStorage_RegardlessPasswordChange(string? password)
    {
        _mockRepository
            .Setup(x => x.SingleOrDefault(IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel());
        _mockMapper
            .Setup(x => x.MapOnlyUpdatedProperties(IsAny<IsAnyType>(), IsAny<UserModel>()))
            .Returns(new UserModel { Password = password! });

        await Sut.UpdateUser(new UserUpdateModel { Password = password! });

        _mockRepository.Verify(x => x.Update(IsAny<UserModel>()), Times.Once);
    }

    protected override UserService GetNewSut()
        => new(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockPasswordHasher.Object,
            _mockTenantContextProvider.Object,
            _mockAuthRepository.Object
        );
}