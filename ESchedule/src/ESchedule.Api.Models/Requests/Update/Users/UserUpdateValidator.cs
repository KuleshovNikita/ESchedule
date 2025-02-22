using ESchedule.Api.Models.Extensions;
using ESchedule.Api.Models.ValidationRules;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using PowerInfrastructure.Extensions;

namespace ESchedule.Api.Models.Requests.Update.Users;

public class UserUpdateValidator : BaseUpdateValidator<UserUpdateModel>
{
    private readonly AgeBoundariesValidationRule _ageRule = new();
    private readonly PasswordValidationRule _passwordRule;

    public UserUpdateValidator(IHostEnvironment environment)
    {
        _passwordRule = new(environment.IsLocal());

        this.NotEmptyUnlessNull(x => x.Name);
        this.NotEmptyUnlessNull(x => x.LastName);
        this.NotEmptyUnlessNull(x => x.FatherName);
        this.NotEmptyUnlessNull(x => x.Login)
            .EmailAddress()
            .MaximumLength(450);
        this.NotEmptyUnlessNull(x => x.Role);
        this.NotEmptyUnlessNull(x => x.GroupId);
        this.NotEmptyUnlessNull(x => x.TenantId);

        _ageRule.Apply(this);
        _passwordRule.Apply(this);
    }
}
