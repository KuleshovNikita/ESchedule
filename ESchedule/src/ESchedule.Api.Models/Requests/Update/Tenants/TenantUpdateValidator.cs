using ESchedule.Api.Models.Extensions;
using FluentValidation;

namespace ESchedule.Api.Models.Requests.Update.Tenants;

public class TenantUpdateValidator : BaseUpdateValidator<TenantUpdateModel>
{
    public TenantUpdateValidator()
    {
        this.NotEmptyUnlessNull(x => x.Name)
            .MaximumLength(450);
    }
}
