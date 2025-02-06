using ESchedule.Api.Models.Requests;
using ESchedule.Business.GroupsLessons;
using ESchedule.Business.Mappers;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Moq;

namespace ESchedule.Business.UnitTests.GroupLessons;

public class GroupLessonsServiceTests : TestBase<GroupLessonsService>
{
    private readonly Mock<IRepository<GroupsLessonsModel>> _mockRepository;
    private readonly Mock<IMainMapper> _mockMapper;
    private readonly Mock<ITenantContextProvider> _mockTenantContextProvider;

    public GroupLessonsServiceTests()
    {
        _mockRepository = new Mock<IRepository<GroupsLessonsModel>>();
        _mockMapper = new Mock<IMainMapper>();
        _mockTenantContextProvider = new Mock<ITenantContextProvider>();

        Sut = GetNewSut();
    }

    [Theory]
    [MemberData(nameof(InvalidInputModels))]
    public async Task InsertMany_Throws_ForInvalidInput(IEnumerable<GroupsLessonsCreateModel> data)
    {
        var action = async () => await Sut.InsertMany(data);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task InsertMany_AttachesCorrectTenantToEachModel()
    {
        var expectedTenant = Guid.NewGuid();
        IEnumerable<GroupsLessonsModel> actualResult = null!;
        var request = new List<GroupsLessonsCreateModel>()
        {
            new GroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
            new GroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
            new GroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
        };
        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(expectedTenant));
        _mockMapper
            .Setup(x => x.Map<GroupsLessonsModel>(It.IsAny<GroupsLessonsCreateModel>()))
            .Returns(new GroupsLessonsModel { LessonId = Guid.NewGuid() });
        _mockRepository
            .Setup(x => x.InsertMany(It.IsAny<IEnumerable<GroupsLessonsModel>>()))
            .Callback(new InvocationAction(x => actualResult = (IEnumerable<GroupsLessonsModel>)x.Arguments[0]));

        await Sut.InsertMany(request);

        actualResult.Select(x => x.TenantId.Should().Be(expectedTenant))
                    .All(x => true);
    }

    public static IEnumerable<object[]> InvalidInputModels()
    {
        yield return new object[] { null! };
        yield return new object[] { Array.Empty<GroupsLessonsCreateModel>() };
    }

    protected override GroupLessonsService GetNewSut()
        => new GroupLessonsService(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockTenantContextProvider.Object
        );
}