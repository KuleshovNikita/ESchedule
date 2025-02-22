using ESchedule.Api.Models.Requests.Create.Schedules.Rules;
using ESchedule.Domain.Schedule.Rules;

namespace ESchedule.Business.ScheduleRules;

public interface IRuleService : IBaseService<RuleModel>
{
    Task<RuleModel> CreateRule(RuleCreateModel request);
}
