using ESchedule.Api.Models.Requests.Update;

namespace ESchedule.Api.Models.Requests.Update.Lessons;

public record LessonUpdateModel : BaseUpdateModel
{
    public string? Title { get; set; } = null!;

    public Guid? TenantId { get; set; } = null!;
}
