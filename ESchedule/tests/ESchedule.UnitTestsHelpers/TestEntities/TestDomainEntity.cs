using ESchedule.Domain;

namespace ESchedule.UnitTestsHelpers.TestEntities;

public record TestDomainEntity : BaseModel
{
    public int Prop1 { get; set; }

    public string Prop2 { get; set; }

    public DateTime Prop3 { get; set; }
}
