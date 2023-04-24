using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Schedule.Rules;
using System.Data;
using System.Reflection;
using System.Text.Json;

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

        public IEnumerable<BaseScheduleRule> ParseToRules(IEnumerable<RuleInputModel> rules)
        {
            var rulesList = new List<BaseScheduleRule>();

            foreach (var rule in rules)
            {
                var targetRuleType = _ruleTypes.FirstOrDefault(r => r.Name.ToLowerInvariant() == rule.RuleName.ToLowerInvariant());

                if(targetRuleType == null)
                {
                    throw new NoSuchRuleException($"The rule with name {rule.RuleName} doesn't exist");
                }

                var ruleInstance = (BaseScheduleRule)JsonSerializer.Deserialize(rule.JsonBody, targetRuleType)!;
                rulesList.Add(ruleInstance);
            }

            return rulesList;
        }

        public IEnumerable<RuleModel> ParseToRulesForUI(IEnumerable<RuleModel> rules)
        {
            var rulesList = new List<RuleModel>();

            foreach (var rule in rules)
            {
                var targetRuleType = _ruleTypes.FirstOrDefault(r => r.Name.ToLowerInvariant() == rule.RuleName.ToLowerInvariant());
                var serializeMethod = targetRuleType!.GetMethod("GetJson");

                if (targetRuleType == null)
                {
                    throw new NoSuchRuleException($"The rule with name {rule.RuleName} doesn't exist");
                }

                var ruleInstance = (BaseScheduleRule)JsonSerializer.Deserialize(rule.RuleJson, targetRuleType)!;
                rule.RuleJson = (string)serializeMethod!.Invoke(ruleInstance, null)!;
            }

            return rulesList;
        }
    }
}
