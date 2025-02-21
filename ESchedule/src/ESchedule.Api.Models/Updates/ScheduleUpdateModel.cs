namespace ESchedule.Api.Models.Updates;

public record ScheduleUpdateModel : BaseUpdateModel
{
    public TimeSpan? StartTime { get; set; } = null!;

    public TimeSpan? EndTime { get; set; } = null!;

    public DayOfWeek? DayOfWeek { get; set; } = null!;

    public Guid? StudyGroupId { get; set; } = null!;

    public Guid? TeacherId { get; set; } = null!;

    public Guid? LessonId { get; set; } = null!;

    public Guid? TenantId { get; set; } = null!;
}
