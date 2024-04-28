using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.ScheduleRules
{
    public class RuleService : BaseService<RuleModel>, IRuleService
    {
        private readonly ITenantContextProvider _tenantContextProvider;

        public RuleService(IRepository<RuleModel> repository, ITenantContextProvider tenantContextProvider, IMapper mapper) : base(repository, mapper)
        {
            _tenantContextProvider = tenantContextProvider;
        }

        public async Task<RuleModel> CreateRule(RuleInputModel request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var mapped = _mapper.Map<RuleModel>(request);
            mapped.Id = Guid.NewGuid();
            mapped.TenantId = _tenantContextProvider.Current.TenantId;

            return await _repository.Insert(mapped);
        }
    }
}
