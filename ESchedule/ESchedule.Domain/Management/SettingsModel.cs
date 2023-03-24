using ESchedule.Domain.Users;

namespace ESchedule.Domain.Management
{
    public record SettingsModel : BaseModel
    {
        public TimeSpan StudyDayStartTime { get; set; }
        public TimeSpan LessonDurationTime { get; set; }
        public TimeSpan BreaksDurationTime { get; set; }

        public Guid CreatorId { get; set; }
        public UserModel Creator { get; set; } = null!;
    }
}
