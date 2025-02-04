namespace ESchedule.Domain.Tenant
{
    public class TenantContext
    {
        public Guid TenantId { get; init; }

        public TenantContext(Guid id) 
        {
            TenantId = id;
        }
    }
}
