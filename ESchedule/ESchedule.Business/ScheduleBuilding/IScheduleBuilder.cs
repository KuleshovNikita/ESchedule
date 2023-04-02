using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule;

namespace ESchedule.Business.ScheduleBuilding
{
    public interface IScheduleBuilder
    {
        HashSet<ScheduleModel> BuildSchedules(ScheduleBuilderHelpData builderData);
    }
}
