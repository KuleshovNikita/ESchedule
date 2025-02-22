using ESchedule.Api.Models.Extensions;
using ESchedule.Api.Models.Requests.Create.Groups;
using ESchedule.Api.Models.Requests.Update.Groups;
using FluentValidation;

namespace ESchedule.Api.Models.ValidationRules;

public class MaxLessonsPerDayValidationRule
{
    private readonly (int, int) _maxLessonsCountPerDayBoundaries = (1, 10);

    public void Apply(AbstractValidator<GroupUpdateModel> validator)
    {
        validator
            .NotEmptyUnlessNull(x => x.MaxLessonsCountPerDay)
            .InclusiveBetween(_maxLessonsCountPerDayBoundaries.Item1, _maxLessonsCountPerDayBoundaries.Item2)
            .WithMessage($"Max lessons count per day value must be between {_maxLessonsCountPerDayBoundaries.Item1} and {_maxLessonsCountPerDayBoundaries.Item2}"); 
    }

    public void Apply(AbstractValidator<GroupCreateModel> validator)
    {
        validator
            .RuleFor(x => x.MaxLessonsCountPerDay)
            .InclusiveBetween(_maxLessonsCountPerDayBoundaries.Item1, _maxLessonsCountPerDayBoundaries.Item2)
            .WithMessage($"Max lessons count per day value must be between {_maxLessonsCountPerDayBoundaries.Item1} and {_maxLessonsCountPerDayBoundaries.Item2}");
    }
}
