using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Schedule.Rules;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESchedule.Business.ScheduleRules
{
    public class RulesParser
    {
        private readonly IEnumerable<Type> _ruleTypes;

        public RulesParser()
        {
            _ruleTypes = Assembly
                            .GetExecutingAssembly()
                            .GetTypes()
                            .Where(x => typeof(BaseScheduleRule).IsAssignableFrom(x));
        }

        public IEnumerable<BaseScheduleRule> ParseToRules(IEnumerable<RuleModel> rules)
        {
            var rulesList = new List<BaseScheduleRule>();

            foreach (var rule in rules)
            {
                var targetRuleType = _ruleTypes.FirstOrDefault(r => GetRuleName(r) == rule.RuleName.ToLowerInvariant());

                if(targetRuleType == null)
                {
                    throw new NoSuchRuleException($"The rule with name {rule.RuleName} doesn't exist");
                }

                var ruleInstance = (BaseScheduleRule)JsonSerializer.Deserialize(rule.RuleJson, targetRuleType)!;
                rulesList.Add(ruleInstance);
            }

            return rulesList;
        }

        private string GetRuleName(Type ruleType)
            => (string)ruleType.GetProperty("Name")!.GetValue(Activator.CreateInstance(ruleType))!;
    }
}
