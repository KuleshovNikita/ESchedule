namespace PowerInfrastructure.UnitTests.Mappers.TestMappingModels;

public record TestUpdateModel
{
    public string? Prop1 { get; set; } = null!;
    public int? Prop2 { get; set; } = null!;
    public Guid? Prop3 { get; set; } = null!;
}
