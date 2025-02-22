namespace ESchedule.Api.Models.Requests.Create.TeachersGroupsLessons;

public record TeachersGroupsLessonsCreateModel
{
    public Guid StudyGroupId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid LessonId { get; set; }
}
