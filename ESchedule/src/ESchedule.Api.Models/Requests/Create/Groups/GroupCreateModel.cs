namespace ESchedule.Api.Models.Requests.Create.Groups;

public class GroupCreateModel
{
    public string Title { get; set; } = null!;

    public int MaxLessonsCountPerDay { get; set; }

    public Guid TenantId { get; set; }
}
