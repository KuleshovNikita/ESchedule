namespace ESchedule.Api.Models.Requests;

public class GroupCreateModel
{
    public string Title { get; set; } = null!;

    public int MaxLessonsCountPerDay { get; set; }

    public Guid TenantId { get; set; }
}
