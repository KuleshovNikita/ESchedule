using AutoMapper;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.Tenant
{
    public class TenantSettingsService : BaseService<TenantSettingsModel>, ITenantSettingsService
    {
        private readonly ITenantContextProvider _tenantContextProvider;

        public TenantSettingsService(
            IRepository<TenantSettingsModel> repository, 
            ITenantContextProvider tenantContextProvider, 
            IMapper mapper) : base(repository, mapper)
        {
            _tenantContextProvider = tenantContextProvider;
        }

        public async Task<List<object>> BuildSchedulesTimeTable() //TODO перенести это в скедьюл сервис
        {
            var tenantSettings = await _repository.FirstOrDefault(x => x.TenantId == _tenantContextProvider.Current.TenantId);

            var currentTime = tenantSettings.StudyDayStartTime;
            var resultList = new List<object>();

            for(var i = 0; i < 10; i++)
            {
                var endTime = currentTime + tenantSettings.LessonDurationTime;
                resultList.Add(new { startTime = currentTime, endTime = endTime });
                currentTime = endTime + tenantSettings.BreaksDurationTime;
            }

            return resultList;
        }
    }
}
