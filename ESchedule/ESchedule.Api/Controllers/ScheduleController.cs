using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : ResultingController<ScheduleModel>
    {
        private readonly IScheduleService _scheduleService; 
        private readonly IBaseService<RuleModel> _rulesService; 

        public ScheduleController(IScheduleService scheduleService, IBaseService<ScheduleModel> baseService
            , IBaseService<RuleModel> rulesService) : base(baseService)
        {
            _scheduleService = scheduleService;
            _rulesService = rulesService;
        }

        [Authorize]
        [HttpPost("{tenantId}")]
        public async Task<ServiceResult<Empty>> BuildSchedule(Guid tenantId, [FromBody] IEnumerable<RuleInputModel> rules)
            => await RunWithServiceResult(async () => await _scheduleService.BuildSchedule(tenantId, rules));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel) //TODO сделать позже, подумать как должно работать
            => await RunWithServiceResult(async () => await _service.UpdateItem(scheduleModel));

        [Authorize]
        [HttpGet("tenant/{tenantId}")]
        public async Task<ServiceResult<IEnumerable<ScheduleModel>>> GetSchedulesForTenant(Guid tenantId)
            => await RunWithServiceResult(async () => await _scheduleService.GetItems(x => x.TenantId == tenantId));

        [Authorize]
        [HttpGet("teacher/{teacherId}")]
        public async Task<ServiceResult<IEnumerable<ScheduleModel>>> GetSchedulesForTeacher(Guid teacherId)
            => await RunWithServiceResult(async () => await _scheduleService.GetItems(x => x.TeacherId == teacherId, includeNavs: true));

        [Authorize]
        [HttpGet("rules/{tenantId}")]
        public async Task<ServiceResult<IEnumerable<RuleModel>>> GetScheduleRules(Guid tenantId)
            => await RunWithServiceResult(async () => await _rulesService.GetItems(x => x.TenantId == tenantId));

        [Authorize]
        [HttpGet("group/{groupId}")]
        public async Task<ServiceResult<IEnumerable<ScheduleModel>>> GetSchedulesForGroup(Guid groupId)
            => await RunWithServiceResult(async () => await _scheduleService.GetItems(x => x.StudyGroupId == groupId));

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task<ServiceResult<Empty>> RemoveSchedule(Guid tenantId)
            => await RunWithServiceResult(async () => await _scheduleService.RemoveWhere(x => x.TenantId == tenantId));
    }
}
