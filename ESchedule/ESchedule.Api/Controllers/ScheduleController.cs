using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.ScheduleBuilding;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : ResultingController<ScheduleModel>
    {
        private readonly IScheduleService _scheduleService; 

        public ScheduleController(IScheduleService scheduleService, IBaseService<ScheduleModel> baseService) : base(baseService)
        {
            _scheduleService = scheduleService;
        }

        [Authorize]
        [HttpPost("{tenantId}")]
        public async Task<ServiceResult<IEnumerable<ScheduleModel>>> BuildSchedule(Guid tenantId)           //TODO в будующем изменить на модель
                                                                                                            //со списком правил для построения
            => await RunWithServiceResult(async () => await _scheduleService.BuildSchedule(tenantId));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel) //TODO сделать позже, подумать как должно работать
            => await RunWithServiceResult(async () => await _service.UpdateItem(scheduleModel));

        [Authorize]
        [HttpGet("{tenantId}")]
        public async Task<ServiceResult<IEnumerable<ScheduleModel>>> GetSchedules(Guid tenantId)
            => await RunWithServiceResult(async () => await _scheduleService.GetItems(x => x.TenantId == tenantId));

        [Authorize]
        [HttpDelete("{tenantId}")]
        public async Task<ServiceResult<Empty>> RemoveSchedule(Guid tenantId)
            => await RunWithServiceResult(async () => await _scheduleService.RemoveWhere(x => x.TenantId == tenantId));
    }
}
