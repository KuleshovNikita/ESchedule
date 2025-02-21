namespace ESchedule.Api.Models.Updates;

public record GroupUpdateModel : BaseUpdateModel
{
    public string? Title { get; set; } = null!;

    public int? MaxLessonsCountPerDay { get; set; } = null!;

    public Guid? TenantId { get; set; } = null!;
}
