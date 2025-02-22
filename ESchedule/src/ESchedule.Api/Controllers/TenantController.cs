using ESchedule.Api.Models.Requests.Create.Tenants;
using ESchedule.Api.Models.Requests.Create.Tenants.RequestAccess;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class TenantController(IBaseService<TenantModel> service, ITenantService tenantService) : BaseController<TenantModel>(service)
{
    [Authorize]
    [HttpPost]
    public async Task CreateTenant([FromBody] TenantCreateModel tenantModel)
        => await tenantService.CreateTenant(tenantModel);

    [Authorize]
    [HttpDelete("acceptAccessRequest/{userId}")]
    public async Task AcceptAccessRequest(Guid userId)
        => await tenantService.AcceptAccessRequest(userId);

    [Authorize]
    [HttpDelete("declineAccessRequest/{userId}")]
    public async Task DeclineAccessRequest(Guid userId)
        => await tenantService.DeclineAccessRequest(userId);

    [Authorize]
    [HttpGet("accessRequests")]
    public async Task<IEnumerable<UserModel>> AccessRequests()
        => await tenantService.GetAccessRequests();

    [Authorize]
    [HttpPost("request")]
    public async Task RequestTenantAccess([FromBody] RequestTenantAccessCreateModel tenantModel)
        => await tenantService.RequestTenantAccess(tenantModel);

    [Authorize]
    [HttpPut]
    public async Task UpdateTenant([FromBody] TenantUpdateModel tenantModel)
        => await service.UpdateItem(tenantModel);

    [Authorize]
    [HttpGet("{tenantId}")]
    public async Task<TenantModel> GetTenants(Guid tenantId)
        => await service.FirstOrDefault(x => x.Id == tenantId);

    [Authorize]
    [HttpDelete("{tenantId}")]
    public async Task RemoveTenant(Guid tenantId)
        => await service.RemoveItem(tenantId);
}
