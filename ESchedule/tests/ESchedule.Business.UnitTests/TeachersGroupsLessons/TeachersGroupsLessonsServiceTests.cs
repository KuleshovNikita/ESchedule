using ESchedule.Api.Models.Requests.Create.TeachersGroupsLessons;
using ESchedule.Business.TeachersGroupsLessons;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Moq;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.UnitTests.TeachersGroupsLessons;

public class TeachersGroupsLessonsServiceTests : TestBase<TeachersGroupsLessonsService>
{
    private readonly Mock<IRepository<TeachersGroupsLessonsModel>> _mockRepository;
    private readonly Mock<IMainMapper> _mockMapper;
    private readonly Mock<ITenantContextProvider> _mockTenantContextProvider;

    public TeachersGroupsLessonsServiceTests()
    {
        _mockRepository = new Mock<IRepository<TeachersGroupsLessonsModel>>();
        _mockMapper = new Mock<IMainMapper>();
        _mockTenantContextProvider = new Mock<ITenantContextProvider>();

        Sut = GetNewSut();
    }

    [Theory]
    [MemberData(nameof(InvalidInputModels))]
    public async Task InsertMany_Throws_ForInvalidInput(IEnumerable<TeachersGroupsLessonsCreateModel> data)
    {
        var action = async () => await Sut.InsertMany(data);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task InsertMany_AttachesCorrectTenantToEachModel()
    {
        var expectedTenant = Guid.NewGuid();
        IEnumerable<TeachersGroupsLessonsModel> actualResult = null!;
        var request = new List<TeachersGroupsLessonsCreateModel>()
        {
            new TeachersGroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
            new TeachersGroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
            new TeachersGroupsLessonsCreateModel { LessonId = Guid.NewGuid() },
        };
        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(expectedTenant));
        _mockMapper
            .Setup(x => x.Map<TeachersGroupsLessonsModel>(It.IsAny<TeachersGroupsLessonsCreateModel>()))
            .Returns(new TeachersGroupsLessonsModel { LessonId = Guid.NewGuid() });
        _mockRepository
            .Setup(x => x.InsertMany(It.IsAny<IEnumerable<TeachersGroupsLessonsModel>>()))
            .Callback(new InvocationAction(x => actualResult = (IEnumerable<TeachersGroupsLessonsModel>)x.Arguments[0]));

        await Sut.InsertMany(request);

        actualResult.Select(x => x.TenantId.Should().Be(expectedTenant))
                    .All(x => true);
    }

    public static IEnumerable<object[]> InvalidInputModels()
    {
        yield return new object[] { null! };
        yield return new object[] { Array.Empty<TeachersGroupsLessonsCreateModel>() };
    }

    protected override TeachersGroupsLessonsService GetNewSut()
        => new TeachersGroupsLessonsService(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockTenantContextProvider.Object
        );
}