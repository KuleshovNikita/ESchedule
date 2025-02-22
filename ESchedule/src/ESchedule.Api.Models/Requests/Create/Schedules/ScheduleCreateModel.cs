namespace ESchedule.Api.Models.Requests.Create.Schedules;

public record ScheduleCreateModel
{
    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public Guid StudyGroupId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid LessonId { get; set; }

    public Guid TenantId { get; set; }
}
