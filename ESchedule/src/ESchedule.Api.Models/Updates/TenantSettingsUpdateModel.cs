namespace ESchedule.Api.Models.Updates
{
    public record TenantSettingsUpdateModel : BaseUpdateModel
    {
        public TimeSpan? StudyDayStartTime { get; set; } = null!;
        public TimeSpan? LessonDurationTime { get; set; } = null!;
        public TimeSpan? BreaksDurationTime { get; set; } = null!;
        public Guid? TenantId { get; set; } = null!;
        public Guid? CreatorId { get; set; } = null!;
    }
}
