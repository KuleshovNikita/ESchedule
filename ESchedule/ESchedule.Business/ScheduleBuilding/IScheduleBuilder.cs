using ESchedule.Domain.Schedule;

namespace ESchedule.Business.ScheduleBuilding
{
    internal interface IScheduleBuilder
    {
        void BuildSchedule(ScheduleBuilderHelpData builderData);
    }
}
