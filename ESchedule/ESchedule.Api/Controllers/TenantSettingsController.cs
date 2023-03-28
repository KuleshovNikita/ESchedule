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

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [Authorize]
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetTenantSettings() => throw new NotImplementedException();

        [Authorize]
        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveTenantSettings(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(tenantId));
    }
}
