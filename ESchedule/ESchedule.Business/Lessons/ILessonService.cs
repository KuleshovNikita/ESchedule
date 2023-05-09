using ESchedule.ServiceResulting;

namespace ESchedule.Business.Lessons
{
    public interface ILessonService
    {
        Task<ServiceResult<Empty>> UpdateLessonsList(IEnumerable<Guid> newLessonsList, Guid tenantId);
    }
}
