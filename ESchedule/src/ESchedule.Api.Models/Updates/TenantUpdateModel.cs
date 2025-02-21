namespace ESchedule.Api.Models.Updates;

public record TenantUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; } = null!;
}
