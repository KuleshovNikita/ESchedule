using ESchedule.Api.Models.Requests.Create.Tenants;
using ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Tenant;

public interface ITenantService : IBaseService<TenantModel>
{
    Task<TenantModel> CreateTenant(TenantCreateModel request);

    Task AcceptAccessRequest(Guid userId);

    Task DeclineAccessRequest(Guid userId);

    Task<IEnumerable<UserModel>> GetAccessRequests();

    Task RequestTenantAccess(RequestTenantAccessCreateModel request);
}
