using FluentValidation;
using Newtonsoft.Json;

namespace ESchedule.Api.Models.Requests.Create.Schedules.Rules;

public class RuleCreateValidator : AbstractValidator<RuleCreateModel>
{
    public RuleCreateValidator()
    {
        RuleFor(x => x.RuleName).NotEmpty();
        RuleFor(x => x.RuleJson)
            .NotEmpty()
            .Must(BeJsonFormat)
            .WithMessage("RuleJson property does not meet JSON format");
    }

    private bool BeJsonFormat(string x)
    {
        try
        {
            var result = JsonConvert.DeserializeObject(x);

            return result != null;
        }
        catch
        {
            return false;
        }
    }
}
