namespace ESchedule.Domain.Tenant
{
    public interface ITenantContextProvider
    {
        void UseContext(TenantContext context);

        TenantContext Current { get; }
    }
}
