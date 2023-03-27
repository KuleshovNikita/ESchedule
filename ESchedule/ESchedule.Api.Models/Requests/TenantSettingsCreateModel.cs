namespace ESchedule.Api.Models.Requests
{
    public record TenantSettingsCreateModel
    {
        public TimeSpan StudyDayStartTime { get; set; }
        public TimeSpan LessonDurationTime { get; set; }
        public TimeSpan BreaksDurationTime { get; set; }
        public Guid TenantId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
