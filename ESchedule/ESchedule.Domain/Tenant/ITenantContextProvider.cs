namespace ESchedule.Domain.Tenant
{
    public interface ITenantContextProvider
    {
        TenantContext Current { get; }
    }
}
