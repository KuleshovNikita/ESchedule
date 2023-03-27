namespace ESchedule.Api.Models.Updates
{
    public record LessonUpdateModel : BaseUpdateModel
    {
        public string? Title { get; set; } = null!;

        public Guid? TenantId { get; set; } = null!;
    }
}
