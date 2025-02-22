using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Tenants;

public class TenantCreateValidator : AbstractValidator<TenantCreateModel>
{
    public TenantCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(450);
        RuleFor(x => x.Settings).NotEmpty();
    }
}
