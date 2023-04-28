using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Users;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantController : ResultingController<TenantModel>
    {
        private readonly IUserService _userService;
        private readonly IBaseService<GroupModel> _groupService;

        public TenantController(IBaseService<TenantModel> service, IUserService userService,
            IBaseService<GroupModel> groupService) : base(service)
        {
            _userService = userService;
            _groupService = groupService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateTenant([FromBody] TenantCreateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(tenantModel));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateTenant([FromBody] TenantUpdateModel tenantModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(tenantModel));

        [Authorize(Policies.DispatcherOnly)]
        [HttpGet("teachers/{tenantId}")]
        public async Task<ServiceResult<IEnumerable<UserModel>>> GetTeachers(Guid tenantId)
            => await RunWithServiceResult(async () => await _userService.Where(x => x.TenantId == tenantId 
                                                                                 && x.Role == Domain.Enums.Role.Teacher));

        [Authorize]
        [HttpGet("groups/{tenantId}")]
        public async Task<ServiceResult<IEnumerable<GroupModel>>> GetTenantGroups(Guid tenantId)
            => await RunWithServiceResult(async () => await _groupService.Where(x => x.TenantId == tenantId));

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<ServiceResult<TenantModel>> GetTenants(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == tenantId));

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task<ServiceResult<Empty>> RemoveTenant(Guid tenantId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(tenantId));
    }
}
