namespace ESchedule.Domain.Tenant;

public record TenantContext(Guid id)
{
    public Guid TenantId { get; init; } = id;
}
