using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.TeachersLessons;

public class TeachersLessonsCreateValidator : AbstractValidator<TeachersLessonsCreateModel>
{
    public TeachersLessonsCreateValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();
        RuleFor(x => x.LessonId).NotEmpty();
    }
}
