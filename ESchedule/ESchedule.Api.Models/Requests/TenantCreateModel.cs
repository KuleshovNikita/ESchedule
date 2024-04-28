using ESchedule.Domain.Tenant;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESchedule.Api.Models.Requests
{
    public record TenantCreateModel
    {
        public string Name { get; set; } = null!;

        [NotMapped]
        public string Login { get; set; } = null!;

        [NotMapped]
        public string Password { get; set; } = null!;
        
        [NotMapped]
        public TenantSettingsCreateModel Settings { get; set; } = null!;
    }
}
