using FluentValidation;

namespace ESchedule.Api.Models.Requests.Update;

public class BaseUpdateValidator<TUpdateEntity> : AbstractValidator<TUpdateEntity>
    where TUpdateEntity : BaseUpdateModel
{
    public BaseUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
