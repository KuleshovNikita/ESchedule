using FluentValidation;
using Microsoft.Extensions.Hosting;
using PowerInfrastructure.Extensions;

namespace ESchedule.Api.Models.Requests.Create.Users;

public class UserCreateValidator : AbstractValidator<UserCreateModel>
{
    private readonly (int, int) _ageBoundaries = (5, 99);
    private const string _passwordRegex = "^(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*(),.?\":{}|<>])(?=.*[a-zA-Z]).{10,}$";

    public UserCreateValidator(IHostEnvironment environment)
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.FatherName).NotEmpty();
        RuleFor(x => x.Login)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Age)
            .InclusiveBetween(_ageBoundaries.Item1, _ageBoundaries.Item2)
            .WithMessage($"Age must be between {_ageBoundaries.Item1} and {_ageBoundaries.Item2}");

        var passwordValidator = RuleFor(x => x.Password).NotEmpty();

        if(!environment.IsLocal())
        {
            passwordValidator.Matches(_passwordRegex);
        }
    }
}
