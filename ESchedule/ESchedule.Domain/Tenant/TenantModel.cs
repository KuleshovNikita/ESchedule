using ESchedule.Domain.Users;

namespace ESchedule.Domain.Tenant
{
    public record TenantModel : BaseModel
    {
        public string TenantName { get; set; } = null!;

        public Guid CreatorId { get; set; }
        public UserModel Creator { get; set; } = null!;

        public TenantSettingsModel Settings { get; set; } = null!;
    }
}
