namespace ESchedule.Api.Models.Requests
{
    public record TenantCreateModel
    {
        public string TenantName { get; set; } = null!;
    }
}
