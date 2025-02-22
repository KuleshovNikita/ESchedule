using ESchedule.Api.Models.Requests.Create.Tenants.Settings;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Domain.Tenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class TenantSettingsController(
    IBaseService<TenantSettingsModel> service, 
    ITenantSettingsService tenantSettingsService
)
    : BaseController<TenantSettingsModel>(service)
{
    [Authorize]
    [HttpPost]
    public async Task CreateTenantSettings([FromBody] TenantSettingsCreateModel tenantModel)
        => await service.CreateItem(tenantModel);

    [Authorize]
    [HttpPut]
    public async Task UpdateTenantSettings([FromBody] TenantSettingsUpdateModel tenantModel)
        => await service.UpdateItem(tenantModel);

    [Authorize]
    [HttpGet("time")]
    public async Task<List<object>> GetTenantScheduleTimes()
        => await tenantSettingsService.BuildSchedulesTimeTable();

    [Authorize]
    [HttpGet("{tenantId}")]
    public async Task<TenantSettingsModel> GetTenantSettings(Guid tenantId)
        => await service.FirstOrDefault(x => x.Id == tenantId);

    [Authorize]
    [HttpDelete("{tenantId}")]
    public async Task RemoveTenantSettings(Guid tenantId)
        => await service.RemoveItem(tenantId);
}
