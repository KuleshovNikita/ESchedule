using AutoMapper;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;

namespace ESchedule.Business.Tenant
{
    public class TenantSettingsService : BaseService<TenantSettingsModel>, ITenantSettingsService
    {
        public TenantSettingsService(IRepository<TenantSettingsModel> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public async Task<List<object>> BuildSchedulesTimeTable(Guid tenantId)
        {
            var tenantSettings = await _repository.First(x => x.TenantId == tenantId);

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
