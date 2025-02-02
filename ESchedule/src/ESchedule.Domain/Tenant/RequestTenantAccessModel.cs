namespace ESchedule.Domain.Tenant
{
    public record RequestTenantAccessModel : BaseModel
    {
        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }
    }
}
