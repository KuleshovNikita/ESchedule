using ESchedule.ServiceResulting;

namespace ESchedule.Business.Lessons
{
    public interface ILessonService
    {
        Task UpdateLessonsList(IEnumerable<Guid> newLessonsList, Guid tenantId);
    }
}
