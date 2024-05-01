using ESchedule.Domain.Lessons.Schedule;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleService : IBaseService<ScheduleModel>
    {
        Task BuildSchedule();

        Task RemoveAll();
    }
}
