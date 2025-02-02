using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Responses;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Business.ScheduleRules;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Policy;
using ESchedule.Domain.Schedule.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : BaseController<ScheduleModel>
    {
        private readonly IScheduleService _scheduleService; 
        private readonly IRuleService _rulesService; 

        public ScheduleController(IScheduleService scheduleService, IBaseService<ScheduleModel> baseService,
            IRuleService rulesService) : base(baseService)
        {
            _scheduleService = scheduleService;
            _rulesService = rulesService;
        }

        [Authorize(Policies.DispatcherOnly)]
        [HttpPost]
        public async Task BuildSchedule()
            => await _scheduleService.BuildSchedule();

        [Authorize(Policies.DispatcherOnly)]
        [HttpPut]
        public async Task UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel) //TODO сделать позже, подумать как должно работать
            => await _service.UpdateItem(scheduleModel);

        [Authorize]
        [HttpGet("teacher/{teacherId}")]
        public async Task<IEnumerable<ScheduleItemResponse>> GetSchedulesForTeacher(Guid teacherId)
            => await _scheduleService.GetMinimalSchedule(x => x.TeacherId == teacherId);

        [Authorize(Policies.DispatcherOnly)]
        [HttpGet("rules")]
        public async Task<IEnumerable<RuleModel>> GetScheduleRules()
            => await _rulesService.GetItems();

        [Authorize]
        [HttpGet("group/{groupId}")]
        public async Task<IEnumerable<ScheduleItemResponse>> GetSchedulesForGroup(Guid groupId)
            => await _scheduleService.GetMinimalSchedule(x => x.StudyGroupId == groupId);

        [Authorize]
        [HttpGet("item/{scheduleId}")]
        public async Task<ScheduleModel> GetScheduleItem(Guid scheduleId)
            => await _scheduleService.FirstOrDefault(x => x.Id == scheduleId);

        [Authorize(Policies.DispatcherOnly)]
        [HttpDelete]
        public async Task RemoveSchedule()
            => await _scheduleService.RemoveAll();

        [Authorize(Policies.DispatcherOnly)]
        [HttpPost("rule")]
        public async Task CreateRule([FromBody] RuleInputModel ruleModel)
           => await _rulesService.CreateRule(ruleModel);

        [Authorize(Policies.DispatcherOnly)]
        [HttpDelete("rule/{ruleId}")]
        public async Task CreateRule(Guid ruleId)
           => await _rulesService.RemoveItem(ruleId);
    }
}
