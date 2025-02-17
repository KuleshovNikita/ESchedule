using ESchedule.Api.Models.Requests;
using ESchedule.Business.Tenant;
using ESchedule.Business.Users;
using ESchedule.DataAccess.Repos;
using ESchedule.DataAccess.Repos.Auth;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using PowerInfrastructure.AutoMapper;
using PowerInfrastructure.Http;
using System.Linq.Expressions;

namespace ESchedule.Business.UnitTests.Tenant;

public class TenantServiceTests : TestBase<TenantService>
{
    private readonly Mock<IRepository<TenantModel>> _mockRepository;
    private readonly Mock<IRepository<RequestTenantAccessModel>> _mockTenantRequestRepo;
    private readonly Mock<IAuthRepository> _mockAuthService;
    private readonly Mock<IMainMapper> _mockMapper;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IClaimsAccessor> _mockClaimAccessor;
    private readonly Mock<ITenantContextProvider> _mockTenantContextProvider;
    private readonly Mock<IRepository<UserModel>> _mockUserRepo;

    public TenantServiceTests()
    {
        _mockRepository = new Mock<IRepository<TenantModel>>();
        _mockTenantRequestRepo = new Mock<IRepository<RequestTenantAccessModel>>();
        _mockAuthService = new Mock<IAuthRepository>();
        _mockMapper = new Mock<IMainMapper>();
        _mockUserService = new Mock<IUserService>();
        _mockClaimAccessor = new Mock<IClaimsAccessor>();
        _mockTenantContextProvider = new Mock<ITenantContextProvider>();
        _mockUserRepo = new Mock<IRepository<UserModel>>();
    }

    [Fact]
    public void CreateTenant_ThrowsForNullArgument()
    {
        var action = async () => await GetNewSut().CreateTenant(null!);

        action.Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public void CreateTenant_ThrowsWhenTenantAlreadyExists()
    {
        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(new TenantModel());
        var sut = GetNewSut();

        var action = async () => await sut.CreateTenant(new TenantCreateModel());

        action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public void CreateTenant_ThrowsWhenTargetUserAlreadyAssignedToTenant()
    {
        TenantModel returnValue = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(returnValue);
        _mockClaimAccessor
            .Setup(x => x.GetRequiredClaimValue(It.IsAny<string>()))
            .Returns(Guid.NewGuid().ToString());
        _mockAuthService
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { TenantId = Guid.NewGuid() });
        var sut = GetNewSut();

        var action = async () => await sut.CreateTenant(new TenantCreateModel());

        action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CreateTenant_InsertsValidEntityIntoStorage()
    {
        var request = new TenantCreateModel();
        TenantModel returnValue = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(returnValue);
        _mockClaimAccessor
            .Setup(x => x.GetRequiredClaimValue(It.IsAny<string>()))
            .Returns(Guid.NewGuid().ToString());
        _mockRepository
            .Setup(x => x.Insert(It.IsAny<TenantModel>()))
            .ReturnsAsync(new TenantModel());
        _mockAuthService
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { TenantId = null! });
        _mockMapper
            .Setup(x => x.Map<TenantModel>(request))
            .Returns(new TenantModel());
        var sut = GetNewSut();

        await sut.CreateTenant(request);

        _mockRepository.Verify(x => x.Insert(It.IsAny<TenantModel>()), Times.Once);
    }

    [Fact]
    public async Task CreateTenant_AttachesUserToTenant()
    {
        var request = new TenantCreateModel();
        TenantModel returnValue = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(returnValue);
        _mockClaimAccessor
            .Setup(x => x.GetRequiredClaimValue(It.IsAny<string>()))
            .Returns(Guid.NewGuid().ToString());
        _mockRepository
            .Setup(x => x.Insert(It.IsAny<TenantModel>()))
            .ReturnsAsync(new TenantModel());
        _mockAuthService
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(new UserModel { TenantId = null! });
        _mockMapper
            .Setup(x => x.Map<TenantModel>(request))
            .Returns(new TenantModel());
        var sut = GetNewSut();

        await sut.CreateTenant(request);

        _mockUserService.Verify(x => x.SignUserToTenant(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task AcceptAccessRequest_ThrowsWhenNoUserFound()
    {
        UserModel userResult = null!;

        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        var action = async () => await sut.AcceptAccessRequest(Guid.NewGuid());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task AcceptAccessRequest_RemovesAllUserRequestsFromDbOnAcception()
    {
        var tenantId = Guid.NewGuid();
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
        };

        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(tenantId));
        _mockTenantRequestRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockTenantRequestRepo.Object);
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        await sut.AcceptAccessRequest(userResult.Id);

        _mockTenantRequestRepo.Verify(x => x.RemoveRange(It.IsAny<IEnumerable<RequestTenantAccessModel>>()), Times.Once);
    }

    [Fact]
    public async Task AcceptAccessRequest_SetsTenantIdToUser()
    {
        var tenantId = Guid.NewGuid();
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
            TenantId = null
        };

        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(tenantId));
        _mockTenantRequestRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockTenantRequestRepo.Object);
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        await sut.AcceptAccessRequest(userResult.Id);

        userResult.TenantId.Should().Be(tenantId);
    }

    [Fact]
    public async Task AcceptAccessRequest_SavesChangesToDatabase()
    {
        var tenantId = Guid.NewGuid();
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
            TenantId = null
        };

        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(tenantId));
        _mockTenantRequestRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockTenantRequestRepo.Object);
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        await sut.AcceptAccessRequest(userResult.Id);

        _mockUserRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeclineAccessRequest_ThrowsWhenNoUserFound()
    {
        UserModel userResult = null!;

        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        var action = async () => await sut.DeclineAccessRequest(Guid.NewGuid());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task DeclineAccessRequest_RemovesUserRequestFromTenantList()
    {
        var tenantId = Guid.NewGuid();
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
        };
        
        _mockTenantRequestRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RequestTenantAccessModel, bool>>>()))
            .ReturnsAsync(new RequestTenantAccessModel { TenantId = tenantId, Id = Guid.NewGuid(), UserId = userResult.Id });
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        await sut.DeclineAccessRequest(userResult.Id);

        _mockTenantRequestRepo.Verify(x => x.Remove(It.IsAny<RequestTenantAccessModel>()), Times.Once);
    }

    [Fact]
    public async Task DeclineAccessRequest_SavesChangesToDatabase()
    {
        var tenantId = Guid.NewGuid();
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
            TenantId = null
        };

        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(tenantId));
        _mockTenantRequestRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockTenantRequestRepo.Object);
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(userResult);
        var sut = GetNewSut();

        await sut.DeclineAccessRequest(userResult.Id);

        _mockUserRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAccessRequests_ThrowsWhenNoUserFound()
    {
        TenantModel tenantResult = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(tenantResult);
        var sut = GetNewSut();

        var action = sut.GetAccessRequests;

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task GetAccessRequests_GetsListOfUsers()
    {
        var tenantId = Guid.NewGuid();
        var tenantRequestsList = new List<RequestTenantAccessModel>
        {
            new() { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TenantId = tenantId },
            new() { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TenantId = tenantId },
            new() { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), TenantId = tenantId },
        };
        var usersList = new List<UserModel>
        {
            new() { Id = tenantRequestsList[0].UserId, TenantId = tenantId },
            new() { Id = Guid.NewGuid(), TenantId = tenantId },
            new() { Id = Guid.NewGuid(), TenantId = Guid.NewGuid() }
        };
        var userResult = new UserModel
        {
            Id = Guid.NewGuid(),
            TenantId = null
        };

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(new TenantModel());
        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(tenantId));
        _mockTenantRequestRepo
            .Setup(x => x.All())
            .ReturnsAsync(tenantRequestsList);
        _mockUserRepo
            .Setup(x => x.IgnoreQueryFilters())
            .Returns(_mockUserRepo.Object);
        _mockUserRepo
            .Setup(x => x.Where(It.IsAny<Expression<Func<UserModel, bool>>>()))
            .ReturnsAsync(usersList);
        var sut = GetNewSut();

        var result = await sut.GetAccessRequests();

        result.Should().BeEquivalentTo(usersList);
    }

    [Fact]
    public async Task RequestTenantAccess_ThrowsForNullRequestModel()
    {
        var sut = GetNewSut();

        var action = async () => await sut.RequestTenantAccess(null!);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task RequestTenantAccess_ThrowsNotFoundIfTenantDoesNotExist()
    {
        TenantModel tenantResult = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(tenantResult);
        var sut = GetNewSut();

        var action = async () => await sut.RequestTenantAccess(new());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task RequestTenantAccess_ThrowsInvalidOperationIfRequestAlreadyExists()
    {
        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(new TenantModel());
        _mockTenantRequestRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RequestTenantAccessModel, bool>>>()))
            .ReturnsAsync(new RequestTenantAccessModel());
        var sut = GetNewSut();

        var action = async () => await sut.RequestTenantAccess(new());

        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task RequestTenantAccess_InsertsRequestIntoDatabase()
    {
        RequestTenantAccessModel requestCheckResult = null!;

        _mockRepository
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<TenantModel, bool>>>()))
            .ReturnsAsync(new TenantModel());
        _mockTenantRequestRepo
            .Setup(x => x.SingleOrDefault(It.IsAny<Expression<Func<RequestTenantAccessModel, bool>>>()))
            .ReturnsAsync(requestCheckResult);
        _mockMapper
            .Setup(x => x.Map<RequestTenantAccessModel>(It.IsAny<RequestTenantAccessModel>()))
            .Returns(new RequestTenantAccessModel());
        var sut = GetNewSut();

        await sut.RequestTenantAccess(new());

        _mockTenantRequestRepo.Verify(x => x.Insert(It.IsAny<RequestTenantAccessModel>()), Times.Once);
    }

    protected override TenantService GetNewSut()
        => new TenantService(
                _mockRepository.Object,
                _mockTenantRequestRepo.Object,
                _mockAuthService.Object,
                _mockMapper.Object,
                _mockUserService.Object,
                _mockClaimAccessor.Object,
                _mockTenantContextProvider.Object,
                _mockUserRepo.Object
        );
}
