namespace ESchedule.Api.Models.Requests.Create.GroupsLessons;

public record GroupsLessonsCreateModel
{
    public Guid LessonId { get; set; }

    public Guid StudyGroupId { get; set; }
}
