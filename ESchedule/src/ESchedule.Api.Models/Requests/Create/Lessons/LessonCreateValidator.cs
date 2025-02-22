using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Lessons;

public class LessonCreateValidator : AbstractValidator<LessonCreateModel>
{
    public LessonCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
    }
}
