using AutoMapper;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business.ScheduleRules
{
    public class RuleService : BaseService<RuleModel>, IRuleService
    {
        public RuleService(IRepository<RuleModel> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task<ServiceResult<IEnumerable<RuleModel>>> GetItems(
            Expression<Func<RuleModel, bool>> predicate,
            bool includeNavs = false)
        {
            var rules = (await base.GetItems(predicate, includeNavs)).Value;
            var parser = new RulesParser();

            return new ServiceResult<IEnumerable<RuleModel>>
            {
                Value = parser.ParseToRulesForUI(rules)
            };
        }
    }
}
