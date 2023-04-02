using ESchedule.Domain.Lessons.Schedule;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleModel>> BuildSchedule(Guid tenantId);
    }
}
