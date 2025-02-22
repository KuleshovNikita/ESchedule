using ESchedule.Api.Models.ValidationRules;
using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Groups;

public class GroupCreateValidator : AbstractValidator<GroupCreateModel>
{
    private readonly MaxLessonsPerDayValidationRule _rule = new();

    public GroupCreateValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(5);
        RuleFor(x => x.TenantId).NotEmpty();
        _rule.Apply(this);
    }
}
