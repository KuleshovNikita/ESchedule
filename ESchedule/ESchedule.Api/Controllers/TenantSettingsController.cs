using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantSettingsController : ResultingController<TenantSettingsModel>
    {
        public TenantSettingsController(IBaseService<TenantSettingsModel> service) : base(service)
        {
        }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateTenantSettings([FromBody] TenantSettingsCreateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(tenantModel));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateTenantSettings([FromBody] TenantSettingsUpdateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(tenantModel));

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<ServiceResult<TenantSettingsModel>> GetTenantSettings(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == tenantId));

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task<ServiceResult<Empty>> RemoveTenantSettings(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(tenantId));
    }
}
