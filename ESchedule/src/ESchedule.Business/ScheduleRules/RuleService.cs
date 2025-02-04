using ESchedule.Api.Models.Requests;
using ESchedule.Business.Mappers;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.ScheduleRules;

public class RuleService : BaseService<RuleModel>, IRuleService
{
    private readonly ITenantContextProvider _tenantContextProvider;

    public RuleService(IRepository<RuleModel> repository, ITenantContextProvider tenantContextProvider, IMainMapper mapper) : base(repository, mapper)
    {
        _tenantContextProvider = tenantContextProvider;
    }

    public async Task<RuleModel> CreateRule(RuleInputModel request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var mapped = _mapper.Map<RuleModel>(request);
        mapped.Id = Guid.NewGuid();
        mapped.TenantId = _tenantContextProvider.Current.TenantId;

        return await _repository.Insert(mapped);
    }
}