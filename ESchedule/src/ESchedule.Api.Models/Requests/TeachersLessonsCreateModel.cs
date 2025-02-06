namespace ESchedule.Api.Models.Requests;

public record TeachersLessonsCreateModel
{
    public Guid LessonId { get; set; }

    public Guid TeacherId { get; set; }
}
