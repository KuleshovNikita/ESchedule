using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Domain.Management
{
    public record SettingsModel : BaseModel
    {
        public TimeSpan StudyDayStartTime { get; set; }
        public TimeSpan LessonDurationTime { get; set; }
        public TimeSpan BreaksDurationTime { get; set; }

        public Guid TenantId { get; set; }
        public TenantModel Tenant { get; set; } = null!;

        public Guid CreatorId { get; set; }
        public UserModel Creator { get; set; } = null!;
    }
}
