namespace ESchedule.Api.Models.Requests.Create.TeachersLessons;

public record TeachersLessonsCreateModel
{
    public Guid LessonId { get; set; }

    public Guid TeacherId { get; set; }
}
