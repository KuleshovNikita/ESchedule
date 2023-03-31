namespace ESchedule.Domain.Tenant
{
    public record TenantModel : BaseModel
    {
        public string TenantName { get; set; } = null!;
    }
}
