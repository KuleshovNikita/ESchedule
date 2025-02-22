namespace ESchedule.Api.Models.Requests.Create.Lessons;

public record LessonCreateModel
{
    public string Title { get; set; } = null!;

    public Guid TenantId { get; set; }
}
