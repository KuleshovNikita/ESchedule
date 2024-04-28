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
    public class TenantSettingsController : BaseController<TenantSettingsModel>
    {
        private readonly ITenantSettingsService _tenantSettingsService;

        public TenantSettingsController(IBaseService<TenantSettingsModel> service, ITenantSettingsService tenantSettingsService) 
            : base(service)
        {
            _tenantSettingsService = tenantSettingsService;
        }

        [Authorize]
        [HttpPost]
        public async Task CreateTenantSettings([FromBody] TenantSettingsCreateModel tenantModel)
            => await _service.CreateItem(tenantModel);

        [Authorize]
        [HttpPut]
        public async Task UpdateTenantSettings([FromBody] TenantSettingsUpdateModel tenantModel)
            => await _service.UpdateItem(tenantModel);

        [Authorize]
        [HttpGet("time")]
        public async Task<List<object>> GetTenantScheduleTimes()
            => await _tenantSettingsService.BuildSchedulesTimeTable();

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<TenantSettingsModel> GetTenantSettings(Guid tenantId)
            => await _service.First(x => x.Id == tenantId);

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task RemoveTenantSettings(Guid tenantId)
            => await _service.RemoveItem(tenantId);
    }
}
