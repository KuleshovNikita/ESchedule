using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Groups;

public class GroupCreateValidator : AbstractValidator<GroupCreateModel>
{
    private readonly (int, int) _maxLessonsCountPerDayBoundaries = (1, 10);

    public GroupCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
        RuleFor(x => x.MaxLessonsCountPerDay)
            .InclusiveBetween(_maxLessonsCountPerDayBoundaries.Item1, _maxLessonsCountPerDayBoundaries.Item2)
            .WithMessage($"Max lessons count per day value must be between {_maxLessonsCountPerDayBoundaries.Item1} and {_maxLessonsCountPerDayBoundaries.Item2}");
    }
}
