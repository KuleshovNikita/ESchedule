namespace ESchedule.Domain.Tenant;

public record TenantSettingsModel : BaseModel
{
    public TimeSpan StudyDayStartTime { get; set; }
    public TimeSpan LessonDurationTime { get; set; }
    public TimeSpan BreaksDurationTime { get; set; }

    public Guid TenantId { get; set; }
    public TenantModel Tenant { get; set; } = null!;
}
