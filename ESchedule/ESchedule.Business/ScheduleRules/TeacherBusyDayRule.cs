using ESchedule.Domain.Lessons.Schedule;

namespace ESchedule.Business.ScheduleRules
{
    public class TeacherBusyDayRule : BaseScheduleRule
    {
        public override string RuleName => nameof(TeacherBusyDayRule);

        public DayOfWeek Target { get; set; }

        public override bool Verify(ScheduleModel schedule)
            => schedule.TeacherId != ActorId && Target != schedule.DayOfWeek;
    }
}
