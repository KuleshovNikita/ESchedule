namespace ESchedule.Domain.Tenant;

public record TenantModel : BaseModel
{
    public string Name { get; set; } = null!;

    public TenantSettingsModel Settings { get; set; } = null!;
}
