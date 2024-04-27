using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Users;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class TenantController : BaseController<TenantModel>
    {
        private readonly IUserService _userService;
        private readonly IBaseService<GroupModel> _groupService;
        private readonly IBaseService<LessonModel> _lessonService;

        public TenantController(IBaseService<TenantModel> service, IUserService userService,
            IBaseService<GroupModel> groupService, IBaseService<LessonModel> lessonService) : base(service)
        {
            _userService = userService;
            _groupService = groupService;       
            _lessonService = lessonService;
        }

        [Authorize]
        [HttpPost]
        public async Task CreateTenant([FromBody] TenantCreateModel tenantModel)
            => await _service.CreateItem(tenantModel);

        [Authorize]
        [HttpPut]
        public async Task UpdateTenant([FromBody] TenantUpdateModel tenantModel)
            => await _service.UpdateItem(tenantModel);

        [Authorize(Policies.DispatcherOnly)]
        [HttpGet("teachers/{tenantId}")]
        public async Task<IEnumerable<UserModel>> GetTeachers(Guid tenantId)
            => await _userService.Where(x => x.TenantId == tenantId && x.Role == Domain.Enums.Role.Teacher); //TODO перенести это в бизнесс

        [Authorize]
        [HttpGet("groups/{tenantId}")]
        public async Task<IEnumerable<GroupModel>> GetTenantGroups(Guid tenantId)
            => await _groupService.Where(x => x.TenantId == tenantId);

        [Authorize]
        [HttpGet("lessons/{tenantId}")]
        public async Task<IEnumerable<LessonModel>> GetTenantLessons(Guid tenantId)
            => await _lessonService.Where(x => x.TenantId == tenantId);

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<TenantModel> GetTenants(Guid tenantId)
            => await _service.First(x => x.Id == tenantId);

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task RemoveTenant(Guid tenantId)
            => await _service.RemoveItem(tenantId);
    }
}
