using ESchedule.Api.Models.Requests.Update;

namespace ESchedule.Api.Models.Requests.Update.Tenants;

public record TenantUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; } = null!;
}
