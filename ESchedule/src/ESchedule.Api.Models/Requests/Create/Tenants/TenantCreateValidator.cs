using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Tenants;

public class TenantCreateValidator : AbstractValidator<TenantCreateModel>
{
    public TenantCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Settings).NotEmpty();
    }
}
