using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : BaseController<ScheduleModel>
    {
        private readonly IScheduleService _scheduleService; 
        private readonly IBaseService<RuleModel> _rulesService; 

        public ScheduleController(IScheduleService scheduleService, IBaseService<ScheduleModel> baseService
            , IBaseService<RuleModel> rulesService) : base(baseService)
        {
            _scheduleService = scheduleService;
            _rulesService = rulesService;
        }

        [Authorize(Policies.DispatcherOnly)]
        [HttpPost("{tenantId}")]
        public async Task BuildSchedule(Guid tenantId)
            => await _scheduleService.BuildSchedule(tenantId);

        [Authorize(Policies.DispatcherOnly)]
        [HttpPut]
        public async Task UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel) //TODO сделать позже, подумать как должно работать
            => await _service.UpdateItem(scheduleModel);

        [Authorize]
        [HttpGet("tenant/{tenantId}")]
        public async Task<IEnumerable<ScheduleModel>> GetSchedulesForTenant(Guid tenantId)
            => await _scheduleService.GetItems(x => x.TenantId == tenantId);

        [Authorize]
        [HttpGet("teacher/{teacherId}")]
        public async Task<IEnumerable<ScheduleModel>> GetSchedulesForTeacher(Guid teacherId)
            => await _scheduleService.GetItems(x => x.TeacherId == teacherId, includeNavs: true);

        [Authorize(Policies.DispatcherOnly)]
        [HttpGet("rules/{tenantId}")]
        public async Task<IEnumerable<RuleModel>> GetScheduleRules(Guid tenantId)
            => await _rulesService.GetItems(x => x.TenantId == tenantId);

        [Authorize]
        [HttpGet("group/{groupId}")]
        public async Task<IEnumerable<ScheduleModel>> GetSchedulesForGroup(Guid groupId)
            => await _scheduleService.GetItems(x => x.StudyGroupId == groupId);

        [Authorize]
        [HttpGet("item/{scheduleId}")]
        public async Task<ScheduleModel> GetScheduleItem(Guid scheduleId)
            => await _scheduleService.First(x => x.Id == scheduleId);

        [Authorize(Policies.DispatcherOnly)]
        [HttpDelete("{tenantId}")]
        public async Task RemoveSchedule(Guid tenantId)
            => await _scheduleService.RemoveWhere(x => x.TenantId == tenantId);

        [Authorize(Policies.DispatcherOnly)]
        [HttpPost("rule")]
        public async Task CreateRule([FromBody] RuleInputModel ruleModel)
           => await _rulesService.CreateItem(ruleModel);

        [Authorize(Policies.DispatcherOnly)]
        [HttpDelete("rule/{ruleId}")]
        public async Task CreateRule(Guid ruleId)
           => await _rulesService.RemoveItem(ruleId);
    }
}
