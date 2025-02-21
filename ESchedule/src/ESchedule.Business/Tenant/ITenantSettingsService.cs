using ESchedule.Domain.Tenant;

namespace ESchedule.Business.Tenant;

public interface ITenantSettingsService : IBaseService<TenantSettingsModel>
{
    Task<List<object>> BuildSchedulesTimeTable();
}
