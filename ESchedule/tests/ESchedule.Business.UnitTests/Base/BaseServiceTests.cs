using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Exceptions;
using ESchedule.UnitTestsHelpers.Infrastructure;
using ESchedule.UnitTestsHelpers.TestEntities;
using FluentAssertions;
using PowerInfrastructure.AutoMapper;
using System.Linq.Expressions;

namespace ESchedule.Business.UnitTests.Base;

public class BaseServiceTests : TestBase<BaseService<TestDomainEntity>>
{
    private readonly Mock<IRepository<TestDomainEntity>> _mockRepo;
    private readonly Mock<IMainMapper> _mockMapper;

    public BaseServiceTests()
    {
        _mockRepo = new Mock<IRepository<TestDomainEntity>>();
        _mockMapper = new Mock<IMainMapper>();

        Sut = GetNewSut();
    }

    [Fact]
    public async Task CreateItem_MapsCreateModelToDomainModel()
    {
        _mockMapper
            .Setup(x => x.Map<TestDomainEntity>(It.IsAny<TestCreateEntity>()))
            .Returns(new TestDomainEntity());

        _ = await Sut.CreateItem(new TestCreateEntity());

        _mockMapper.Verify(x => x.Map<It.IsAnyType>(It.IsAny<TestCreateEntity>()), Times.Once);
    }


    [Fact]
    public async Task CreateItem_InsertsDomainModelIntoStorage()
    {
        var domainModel = new TestDomainEntity
        {
            Id = Guid.Empty
        };
        _mockMapper
            .Setup(x => x.Map<TestDomainEntity>(It.IsAny<TestCreateEntity>()))
            .Returns(domainModel);
        _mockRepo
            .Setup(x => x.Insert(domainModel))
            .ReturnsAsync(domainModel);

        _ = await Sut.CreateItem(new TestCreateEntity());

        _mockRepo.Verify(x => x.Insert(domainModel), Times.Once);
    }

    [Fact]
    public async Task CreateItem_ReturnsInsertedModel()
    {
        var domainModel = new TestDomainEntity();
        _mockMapper
            .Setup(x => x.Map<TestDomainEntity>(It.IsAny<TestCreateEntity>()))
            .Returns(domainModel);
        _mockRepo
            .Setup(x => x.Insert(domainModel))
            .ReturnsAsync(domainModel);

        var result = await Sut.CreateItem(new TestCreateEntity());

        result.Should().Be(domainModel);
    }

    [Fact]
    public async Task CreateItem_GeneratesNewIdForDomainModel()
    {
        var domainModel = new TestDomainEntity
        {
            Id = Guid.Empty
        };
        _mockMapper
            .Setup(x => x.Map<TestDomainEntity>(It.IsAny<TestCreateEntity>()))
            .Returns(domainModel);
        _mockRepo
            .Setup(x => x.Insert(domainModel))
            .ReturnsAsync(domainModel);

        var result = await Sut.CreateItem(new TestCreateEntity());

        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task InsertMany_MapsCreateModelsToDomain()
    {
        var createCollection = Array.Empty<TestCreateEntity>();

        await Sut.InsertMany(createCollection);

        _mockMapper.Verify(x => x.Map<It.IsAnyType>(createCollection), Times.Once);
    }

    [Fact]
    public async Task RemoveItem_Throws_IfDoesNotExistById()
    {
        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(false);

        var action = async () => await Sut.RemoveItem(Guid.NewGuid());

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task RemoveItem_Throws_IfDoesNotExistByEntity()
    {
        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(false);

        var action = async () => await Sut.RemoveItem(new TestDomainEntity { Id = Guid.NewGuid() });

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task RemoveItem_CallsRemove()
    {
        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(true);

        await Sut.RemoveItem(Guid.NewGuid());

        _mockRepo.Verify(x => x.Remove(It.IsAny<TestDomainEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateItem_Throws_IfDoesNotExist()
    {
        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(false);

        var action = async () => await Sut.UpdateItem(new TestUpdateEntity { Id = Guid.NewGuid() });

        await action.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task UpdateItem_MapsUpdateEntityToDomainEntity()
    {
        var updateModel = new TestUpdateEntity { Id = Guid.NewGuid() };
        var domainModel = new TestDomainEntity();

        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(true);
        _mockRepo
            .Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(domainModel);

        await Sut.UpdateItem(updateModel);

        _mockMapper.Verify(x => x.MapOnlyUpdatedProperties(updateModel, domainModel), Times.Once);
    }

    [Fact]
    public async Task UpdateItem_CallsUpdate()
    {
        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(true);

        await Sut.UpdateItem(new TestUpdateEntity { Id = Guid.NewGuid() });

        _mockRepo.Verify(x => x.Update(It.IsAny<TestDomainEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateItem_ReturnsUpdatedEntity()
    {
        var updateModel = new TestUpdateEntity { Id = Guid.NewGuid() };
        var domainModel = new TestDomainEntity();

        _mockRepo
            .Setup(x => x.Any(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(true);
        _mockRepo
            .Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<TestDomainEntity, bool>>>()))
            .ReturnsAsync(domainModel);
        _mockMapper
            .Setup(x => x.MapOnlyUpdatedProperties(updateModel, domainModel))
            .Returns(domainModel);

        var result = await Sut.UpdateItem(updateModel);

        result.Should().Be(domainModel);
    }

    protected override BaseService<TestDomainEntity> GetNewSut()
        => new (
            _mockRepo.Object,
            _mockMapper.Object
        );
}
