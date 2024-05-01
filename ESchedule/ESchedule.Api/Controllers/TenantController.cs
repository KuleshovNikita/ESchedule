using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Tenant;
using ESchedule.Business.Users;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantController : BaseController<TenantModel>
    {
        private readonly IUserService _userService;
        private readonly ITenantService _tenantService;
        private readonly IBaseService<GroupModel> _groupService;
        private readonly IBaseService<LessonModel> _lessonService;

        public TenantController(IBaseService<TenantModel> service, IUserService userService,
            IBaseService<GroupModel> groupService, IBaseService<LessonModel> lessonService,
            ITenantService tenantService) : base(service)
        {
            _userService = userService;
            _groupService = groupService;       
            _lessonService = lessonService;
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
