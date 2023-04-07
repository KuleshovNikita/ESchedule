namespace ESchedule.Api.Models.Requests
{
    public record RuleInputModel
    {
        public string RuleName { get; set; } = null!;

        public string JsonBody { get; set; } = null!;
    }
}
