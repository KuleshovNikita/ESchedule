using ESchedule.Api.Models.ValidationRules;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using PowerInfrastructure.Extensions;

namespace ESchedule.Api.Models.Requests.Create.Users;

public class UserCreateValidator : AbstractValidator<UserCreateModel>
{
    private readonly AgeBoundariesValidationRule _ageRule = new();
    private readonly PasswordValidationRule _passwordRule;

    public UserCreateValidator(IHostEnvironment environment)
    {
        _passwordRule = new(environment.IsLocal());

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.FatherName).NotEmpty();
        RuleFor(x => x.Login)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(450);

        _ageRule.Apply(this);
        _passwordRule.Apply(this);
    }
}
