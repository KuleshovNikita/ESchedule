using FluentValidation;
using System.Linq.Expressions;

namespace ESchedule.Api.Models.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> NotEmptyUnlessNull<T, TProperty>(this AbstractValidator<T> rule, Expression<Func<T, TProperty>> predicate)
    {
        return rule.RuleFor(predicate)
                .NotEmpty()
                .When(x => predicate.Compile().Invoke(x) != null);
    }
}