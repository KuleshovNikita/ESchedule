using ESchedule.Api.Models.Updates;
using FluentAssertions;
using System.Reflection;

namespace ESchedule.Api.Models.UnitTests.StaticAnalyzers;

public class UpdateModelsStaticAnalyzer
{
    [Fact]
    public void UpdateModels_ShouldHaveNullableProperties_ButId()
    {
        var modelsAssembly = Assembly.GetAssembly(typeof(BaseUpdateModel));
        var updateModels = modelsAssembly!
                            .GetTypes()
                            .Where(x => x.IsAssignableTo(typeof(BaseUpdateModel)) && x != typeof(BaseUpdateModel));

        foreach (var model in updateModels)
        {
            var props = model
                        .GetProperties()
                        .Where(x => x.Name != "Id");

            props
                .All(x => new NullabilityInfoContext().Create(x).ReadState == NullabilityState.Nullable)
                .Should()
                .BeTrue("because all properties of update models except for id should be nullable");
        }
    }
}
