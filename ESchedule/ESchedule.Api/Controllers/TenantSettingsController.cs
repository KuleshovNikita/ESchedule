using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantSettingsController : ResultingController<TenantSettingsModel>
    {
        private readonly ITenantSettingsService _tenantSettingsService;

        public TenantSettingsController(IBaseService<TenantSettingsModel> service, ITenantSettingsService tenantSettingsService) 
            : base(service)
        {
            _tenantSettingsService = tenantSettingsService;
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
        [HttpGet("time/{tenantId}")]
        public async Task<ServiceResult<List<object>>> GetTenantScheduleTimes(Guid tenantId)
            => await RunWithServiceResult(async () => await _tenantSettingsService.BuildSchedulesTimeTable(tenantId));

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
