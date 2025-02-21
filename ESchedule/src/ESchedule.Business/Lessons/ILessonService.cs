namespace ESchedule.Business.Lessons;

public interface ILessonService
{
    Task RemoveLessons(IEnumerable<Guid> newLessonsList);
}
