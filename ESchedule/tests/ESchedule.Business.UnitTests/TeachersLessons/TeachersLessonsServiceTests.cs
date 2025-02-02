using ESchedule.Api.Models.Requests;
using ESchedule.Business.Mappers;
using ESchedule.Business.TeachersLessons;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using Moq;

namespace ESchedule.Business.UnitTests.TeachersLessons;

public class TeachersLessonsServiceTests : TestBase<TeachersLessonsService>
{
    private readonly Mock<IRepository<TeachersLessonsModel>> _mockRepository;
    private readonly Mock<IMainMapper> _mockMapper;
    private readonly Mock<ITenantContextProvider> _mockTenantContextProvider;

    public TeachersLessonsServiceTests()
    {
        _mockRepository = new Mock<IRepository<TeachersLessonsModel>>();
        _mockMapper = new Mock<IMainMapper>();
        _mockTenantContextProvider = new Mock<ITenantContextProvider>();

        Sut = GetNewSut();
    }

    [Theory]
    [MemberData(nameof(InvalidInputModels))]
    public async Task InsertMany_Throws_ForInvalidInput(IEnumerable<TeachersLessonsCreateModel> data)
    {
        var action = async () => await Sut.InsertMany(data);

        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task InsertMany_AttachesCorrectTenantToEachModel()
    {
        var expectedTenant = Guid.NewGuid();
        IEnumerable<TeachersLessonsModel> actualResult = null!;
        var request = new List<TeachersLessonsCreateModel>()
        {
            new TeachersLessonsCreateModel { LessonId = Guid.NewGuid() },
            new TeachersLessonsCreateModel { LessonId = Guid.NewGuid() },
            new TeachersLessonsCreateModel { LessonId = Guid.NewGuid() },
        };
        _mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(new TenantContext(expectedTenant));
        _mockMapper
            .Setup(x => x.Map<TeachersLessonsModel>(It.IsAny<TeachersLessonsCreateModel>()))
            .Returns(new TeachersLessonsModel { LessonId = Guid.NewGuid() });
        _mockRepository
            .Setup(x => x.InsertMany(It.IsAny<IEnumerable<TeachersLessonsModel>>()))
            .Callback(new InvocationAction(x => actualResult = (IEnumerable<TeachersLessonsModel>)x.Arguments[0]));

        await Sut.InsertMany(request);

        actualResult.Select(x => x.TenantId.Should().Be(expectedTenant))
                    .All(x => true);
    }

    public static IEnumerable<object[]> InvalidInputModels()
    {
        yield return new object[] { null! };
        yield return new object[] { Array.Empty<TeachersLessonsCreateModel>() };
    }

    protected override TeachersLessonsService GetNewSut()
        => new TeachersLessonsService(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockTenantContextProvider.Object
        );
}
