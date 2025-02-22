using ESchedule.Api.Models.Requests.Update;

namespace ESchedule.Api.Models.Requests.Update.Groups;

public record GroupUpdateModel : BaseUpdateModel
{
    public string? Title { get; set; } = null!;

    public int? MaxLessonsCountPerDay { get; set; } = null!;

    public Guid? TenantId { get; set; } = null!;
}
