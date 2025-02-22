using ESchedule.Api.Models.Extensions;

namespace ESchedule.Api.Models.Requests.Update.Schedules;

public class ScheduleUpdateValidator : BaseUpdateValidator<ScheduleUpdateModel>
{
    public ScheduleUpdateValidator()
    {
        this.NotEmptyUnlessNull(x => x.StudyGroupId);
        this.NotEmptyUnlessNull(x => x.EndTime);
        this.NotEmptyUnlessNull(x => x.StartTime);
        this.NotEmptyUnlessNull(x => x.LessonId);
        this.NotEmptyUnlessNull(x => x.TeacherId);
        this.NotEmptyUnlessNull(x => x.TenantId);
        this.NotEmptyUnlessNull(x => x.DayOfWeek);
    }
}
