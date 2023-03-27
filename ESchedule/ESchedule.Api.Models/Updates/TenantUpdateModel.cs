namespace ESchedule.Api.Models.Updates
{
    public record TenantUpdateModel : BaseUpdateModel
    {
        public string? TenantName { get; set; } = null!;
    }
}
