using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantController : ResultingController<TenantModel>
    {
        public TenantController(IBaseService<TenantModel> service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateTenant([FromBody] TenantCreateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(tenantModel));

        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateTenant([FromBody] TenantUpdateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(tenantModel));

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetTenants() => throw new NotImplementedException();

        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveTenant(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(tenantId));
    }
}
