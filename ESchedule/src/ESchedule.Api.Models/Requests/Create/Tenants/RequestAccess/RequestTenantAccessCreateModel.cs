namespace ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;

public class RequestTenantAccessCreateModel
{
    public Guid TenantId { get; set; }

    public Guid UserId { get; set; }
}
