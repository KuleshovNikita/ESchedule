using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;

public class RequestTenantAccessCreateValidator : AbstractValidator<RequestTenantAccessCreateModel>
{
    public RequestTenantAccessCreateValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TenantId).NotEmpty();
    }
}
