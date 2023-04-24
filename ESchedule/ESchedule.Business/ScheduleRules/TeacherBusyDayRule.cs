using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Users;

namespace ESchedule.Business.ScheduleRules
{
    public class TeacherBusyDayRule : BaseScheduleRule
    {
        public override string Name => nameof(TeacherBusyDayRule);

        public DayOfWeek Target { get; set; }

        public override bool Verify(ScheduleModel schedule)
            => schedule.TeacherId != ActorId && Target != schedule.DayOfWeek;
    }
}
