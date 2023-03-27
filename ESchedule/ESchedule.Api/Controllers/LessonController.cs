using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class LessonController : ResultingController<LessonModel>
    {
        public LessonController(IBaseService<LessonModel> lessonService) : base(lessonService) { }

        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateLesson([FromBody] LessonCreateModel lessonModel) 
            => await RunWithServiceResult(async () => await _service.CreateItem(lessonModel));

        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateLesson([FromBody] LessonUpdateModel lessonModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(lessonModel));

        // написать логику для выборки нескольки предметов вместо одного по айдишнику
        [HttpGet]
        public async Task<ServiceResult<Empty>> GetLessons() => throw new NotImplementedException();

        [HttpDelete]
        public async Task<ServiceResult<Empty>> RemoveLesson(Guid lessonId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(lessonId));
    }
}
