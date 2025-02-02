namespace ESchedule.Api.Models.Requests
{
    public class RequestTenantAccessCreateModel
    {
        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }
    }
}
