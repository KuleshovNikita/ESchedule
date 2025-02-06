using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Tenant;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.Tenant;

public class TenantSettingsService(
    IRepository<TenantSettingsModel> repository,
    ITenantContextProvider tenantContextProvider,
    IMainMapper mapper
)
    : BaseService<TenantSettingsModel>(repository, mapper), ITenantSettingsService
{
    public async Task<List<object>> BuildSchedulesTimeTable() //TODO перенести это в скедьюл сервис
    {
        var tenantSettings = await Repository.FirstOrDefault(x => x.TenantId == tenantContextProvider.Current.TenantId);

        var currentTime = tenantSettings.StudyDayStartTime;
        var resultList = new List<object>();

        for (var i = 0; i < 10; i++)
        {
            var endTime = currentTime + tenantSettings.LessonDurationTime;
            resultList.Add(new { startTime = currentTime, endTime = endTime });
            currentTime = endTime + tenantSettings.BreaksDurationTime;
        }

        return resultList;
    }
}