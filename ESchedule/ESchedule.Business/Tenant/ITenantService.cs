using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.Tenant
{
    public interface ITenantService : IBaseService<TenantModel>
    {
        Task<TenantModel> CreateTenant(TenantCreateModel request);

        Task<TenantSettingsModel> CreateTenantSettings(TenantSettingsModel request);

        Task RequestTenantAccess(RequestTenantAccessCreateModel request);
    }
}
