using ESchedule.Api.Models.Extensions;
using ESchedule.Api.Models.Requests.Create.Users;
using ESchedule.Api.Models.Requests.Update.Users;
using FluentValidation;

namespace ESchedule.Api.Models.ValidationRules;

public class AgeBoundariesValidationRule
{
    private readonly (int, int) _ageBoundaries = (5, 99);

    public void Apply(AbstractValidator<UserCreateModel> validator)
    {
       validator
            .RuleFor(x => x.Age)
            .InclusiveBetween(_ageBoundaries.Item1, _ageBoundaries.Item2)
            .WithMessage($"Age must be between {_ageBoundaries.Item1} and {_ageBoundaries.Item2}");
    }

    public void Apply(AbstractValidator<UserUpdateModel> validator)
    {
        validator
             .NotEmptyUnlessNull(x => x.Age)
             .InclusiveBetween(_ageBoundaries.Item1, _ageBoundaries.Item2)
             .WithMessage($"Age must be between {_ageBoundaries.Item1} and {_ageBoundaries.Item2}");
    }
}
