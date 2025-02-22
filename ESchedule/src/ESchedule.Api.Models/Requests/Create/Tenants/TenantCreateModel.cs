using ESchedule.Api.Models.Requests.Create.Tenants.Settings;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESchedule.Api.Models.Requests.Create.Tenants;

public record TenantCreateModel
{
    public string Name { get; set; } = null!;

    [NotMapped]
    public TenantSettingsCreateModel Settings { get; set; } = null!;
}
