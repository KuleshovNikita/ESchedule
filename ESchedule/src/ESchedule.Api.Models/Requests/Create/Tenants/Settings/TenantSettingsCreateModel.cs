namespace ESchedule.Api.Models.Requests.Create.Tenants.Settings;

public record TenantSettingsCreateModel
{
    public TimeSpan StudyDayStartTime { get; set; }
    public TimeSpan LessonDurationTime { get; set; }
    public TimeSpan BreaksDurationTime { get; set; }
}
