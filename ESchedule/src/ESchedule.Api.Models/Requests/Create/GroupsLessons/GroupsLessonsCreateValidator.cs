using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.GroupsLessons;

public class GroupsLessonsCreateValidator : AbstractValidator<GroupsLessonsCreateModel>
{
    public GroupsLessonsCreateValidator()
    {
        RuleFor(x => x.StudyGroupId).NotEmpty();
        RuleFor(x => x.LessonId).NotEmpty();
    }
}
