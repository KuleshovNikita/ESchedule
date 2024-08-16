using ESchedule.Api.Models.Requests;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Tenant
{
    public interface ITenantService : IBaseService<TenantModel>
    {
        Task<TenantModel> CreateTenant(TenantCreateModel request);

        Task<TenantSettingsModel> CreateTenantSettings(TenantSettingsModel request);

        Task<IEnumerable<UserModel>> GetAccessRequests();

        Task RequestTenantAccess(RequestTenantAccessCreateModel request);
    }
}
