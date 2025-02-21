using AutoMapper;
using PowerInfrastructure.UnitTests.Mappers.TestMappingModels;
using ESchedule.UnitTestsHelpers.Infrastructure;
using FluentAssertions;
using PowerInfrastructure.AutoMapper;

namespace PowerInfrastructure.UnitTests.Mappers;

public class MainMapperTests : TestBase<MainMapper>
{
    private readonly Mock<MapperConfiguration> _mockConfigurationProvider;

    public MainMapperTests()
    {
        _mockConfigurationProvider = new Mock<MapperConfiguration>(new MapperConfigurationExpression());
    }

    [Fact]
    public void MapOnlyUpdatedProperties_SkipsPropertiesWithNullsInUpdateModel()
    {
        var domainModel = new TestDomainModel
        {
            Prop1 = "old-string",
            Prop2 = 1000,
            Prop3 = Guid.NewGuid(),
        };

        var updateModel = new TestUpdateModel
        {
            Prop1 = "new-string",
            Prop2 = 2000,
            Prop3 = null
        };

        var expectedResultModel = new TestDomainModel
        {
            Prop1 = "new-string",
            Prop2 = 2000,
            Prop3 = domainModel.Prop3
        };

        var actualResultModel = GetNewSut().MapOnlyUpdatedProperties(updateModel, domainModel);

        actualResultModel.Should().BeEquivalentTo(expectedResultModel);
    }

    protected override MainMapper GetNewSut()
        => new(_mockConfigurationProvider.Object);
}
