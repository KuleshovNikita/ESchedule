using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Domain.Tenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantController : BaseController<TenantModel>
    {
        private readonly ITenantService _tenantService;

        public TenantController(IBaseService<TenantModel> service, ITenantService tenantService) : base(service)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public async Task CreateTenant([FromBody] TenantCreateModel tenantModel)
            => await _tenantService.CreateTenant(tenantModel);

        [Authorize]
        [HttpPut]
        public async Task UpdateTenant([FromBody] TenantUpdateModel tenantModel)
            => await _service.UpdateItem(tenantModel);

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<TenantModel> GetTenants(Guid tenantId)
            => await _service.FirstOrDefault(x => x.Id == tenantId);

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task RemoveTenant(Guid tenantId)
            => await _service.RemoveItem(tenantId);
    }
}
