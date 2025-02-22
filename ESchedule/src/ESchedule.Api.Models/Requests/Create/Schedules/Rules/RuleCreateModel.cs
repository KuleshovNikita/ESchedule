namespace ESchedule.Api.Models.Requests.Create.Schedules.Rules;

public record RuleCreateModel
{
    public string RuleName { get; set; } = null!;

    public string RuleJson { get; set; } = null!;
}
