using ESchedule.Api.Models.Requests.Create.Schedules.Rules;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.ScheduleRules;

public class RuleService(
    IRepository<RuleModel> repository, 
    ITenantContextProvider tenantContextProvider, 
    IMainMapper mapper
)
    : BaseService<RuleModel>(repository, mapper), IRuleService
{
    public async Task<RuleModel> CreateRule(RuleCreateModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var mapped = Mapper.Map<RuleModel>(request);
        mapped.Id = Guid.NewGuid();
        mapped.TenantId = tenantContextProvider.Current.TenantId;

        return await Repository.Insert(mapped);
    }
}