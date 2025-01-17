﻿using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
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

        [Authorize]
        [HttpPost]
        public async Task CreateTenant([FromBody] TenantCreateModel tenantModel)
            => await _tenantService.CreateTenant(tenantModel);

        [Authorize]
        [HttpDelete("acceptAccessRequest/{userId}")]
        public async Task AcceptAccessRequest(Guid userId)
            => await _tenantService.AcceptAccessRequest(userId);

        [Authorize]
        [HttpDelete("declineAccessRequest/{userId}")]
        public async Task DeclineAccessRequest(Guid userId)
            => await _tenantService.DeclineAccessRequest(userId);

        [Authorize]
        [HttpGet("accessRequests")]
        public async Task<IEnumerable<UserModel>> AccessRequests()
            => await _tenantService.GetAccessRequests();

        [Authorize]
        [HttpPost("request")]
        public async Task RequestTenantAccess([FromBody] RequestTenantAccessCreateModel tenantModel)
            => await _tenantService.RequestTenantAccess(tenantModel);

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
