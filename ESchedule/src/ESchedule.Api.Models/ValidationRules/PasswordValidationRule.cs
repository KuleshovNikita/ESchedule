using ESchedule.Api.Models.Extensions;
using ESchedule.Api.Models.Requests.Create.Users;
using ESchedule.Api.Models.Requests.Update.Users;
using FluentValidation;

namespace ESchedule.Api.Models.ValidationRules;

public class PasswordValidationRule(bool isLocalEnv)
{
    private const string _passwordRegex = "^(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*(),.?\":{}|<>])(?=.*[a-zA-Z]).{10,}$";

    public void Apply(AbstractValidator<UserCreateModel> validator)
    {
        var modifiedValidator = validator.RuleFor(x => x.Password).NotEmpty();

        if (!isLocalEnv)
        {
            modifiedValidator.Matches(_passwordRegex);
        }
    }

    public void Apply(AbstractValidator<UserUpdateModel> validator)
    {
        var modifiedValidator = validator.NotEmptyUnlessNull(x => x.Password);

        if (!isLocalEnv)
        {
            modifiedValidator.Matches(_passwordRegex);
        }
    }
}
