using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Schedule.Rules;

namespace ESchedule.Business.ScheduleRules
{
    public interface IRuleService : IBaseService<RuleModel>
    {
        Task<RuleModel> CreateRule(RuleInputModel request);
    }
}
