using ESchedule.Api.Models.Requests.Update;

namespace ESchedule.UnitTestsHelpers.TestEntities;

public record TestUpdateEntity : BaseUpdateModel
{
    public int? Prop1 { get; set; }

    public string? Prop2 { get; set; }

    public DateTime? Prop3 { get; set; }
}
