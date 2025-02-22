using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.TeachersGroupsLessons;

public class TeacherGroupsLessonsCreateValidator : AbstractValidator<TeachersGroupsLessonsCreateModel>
{
    public TeacherGroupsLessonsCreateValidator()
    {
        RuleFor(x => x.StudyGroupId).NotEmpty();
        RuleFor(x => x.TeacherId).NotEmpty();
        RuleFor(x => x.LessonId).NotEmpty();
    }
}
