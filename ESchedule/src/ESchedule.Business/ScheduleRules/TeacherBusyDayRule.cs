using ESchedule.Domain.Lessons.Schedule;

namespace ESchedule.Business.ScheduleRules;

public class TeacherBusyDayRule : BaseScheduleRule
{
    public override string Name => "rules.teacher-busy-day";

    public string Target { get; set; } = null!;

    public override bool Verify(ScheduleModel schedule)
        => schedule.TeacherId != ActorId || schedule.TeacherId == ActorId && (DayOfWeek)int.Parse(Target) != schedule.DayOfWeek;
}
