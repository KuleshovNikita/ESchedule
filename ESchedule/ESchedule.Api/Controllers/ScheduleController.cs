using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class ScheduleController : ResultingController<ScheduleModel>
    {
        public ScheduleController(IBaseService<ScheduleModel> service) : base(service)
        {
        }

        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateSchedule([FromBody] ScheduleCreateModel scheduleModel)
            => await RunWithServiceResult(async () => await _service.CreateItem(scheduleModel));

        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateSchedule([FromBody] ScheduleUpdateModel scheduleModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(scheduleModel));

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetSchedules() => throw new NotImplementedException();

        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveSchedule(Guid scheduleId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(scheduleId));
    }
}
