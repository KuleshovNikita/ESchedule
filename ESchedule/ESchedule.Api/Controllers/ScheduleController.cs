using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : ResultingController<ScheduleModel>
    {
        public ScheduleController(IBaseService<ScheduleModel> service) : base(service)
        {
        }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateSchedule([FromBody] ScheduleCreateModel scheduleModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(scheduleModel));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(scheduleModel));

        [Authorize]
        [HttpGet("{scheduleId}")]
        public async Task<ServiceResult<ScheduleModel>> GetSchedules(Guid scheduleId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == scheduleId));

        [Authorize]
        [HttpDelete("{scheduleId}")]
        public async Task<ServiceResult<Empty>> RemoveSchedule(Guid scheduleId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(scheduleId));
    }
}
