using ESchedule.Api.Models.Extensions;
using ESchedule.Api.Models.ValidationRules;
using FluentValidation;

namespace ESchedule.Api.Models.Requests.Update.Groups;

public class GroupUpdateValidator : BaseUpdateValidator<GroupUpdateModel>
{
    private readonly MaxLessonsPerDayValidationRule _rule = new();

    public GroupUpdateValidator()
    {
        this.NotEmptyUnlessNull(x => x.Title)
            .MaximumLength(5);
        this.NotEmptyUnlessNull(x => x.TenantId);

        _rule.Apply(this);
    }
}
