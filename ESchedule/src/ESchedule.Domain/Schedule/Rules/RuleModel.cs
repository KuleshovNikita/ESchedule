using ESchedule.Domain.Tenant;

namespace ESchedule.Domain.Schedule.Rules
{
    public record RuleModel : BaseModel
    {
        public string RuleName { get; set; } = null!;

        public string RuleJson { get; set; } = null!;

        public Guid TenantId { get; set; }
        public TenantModel Tenant { get; set; } = null!;
    }
}
