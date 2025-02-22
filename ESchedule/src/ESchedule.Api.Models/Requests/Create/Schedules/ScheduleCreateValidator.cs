using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Schedules;

public class ScheduleCreateValidator : AbstractValidator<ScheduleCreateModel>
{
    public ScheduleCreateValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
        RuleFor(x => x.StudyGroupId).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).NotEmpty();
        RuleFor(x => x.LessonId).NotEmpty();
    }
}
