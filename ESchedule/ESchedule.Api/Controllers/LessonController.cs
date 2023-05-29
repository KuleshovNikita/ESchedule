using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Lessons;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class LessonController : ResultingController<LessonModel>
    {
        private readonly ILessonService _lessonService;
        private readonly IAttendanceService _attendanceService;

        public LessonController(IBaseService<LessonModel> baseLessonService, ILessonService lessonService,
            IAttendanceService attendanceService) : base(baseLessonService)
        {
            _lessonService = lessonService;
            _attendanceService = attendanceService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ServiceResult<Empty>> CreateLesson([FromBody] LessonCreateModel lessonModel) 
            => await RunWithServiceResult(async () => await _service.CreateItem(lessonModel));

        [Authorize]
        [HttpPut("many/{tenantId}")]
        public async Task<ServiceResult<Empty>> UpdateLessonsList([FromBody] IEnumerable<Guid> newLessonsList, Guid tenantId)
            => await RunWithServiceResult(async () => await _lessonService.UpdateLessonsList(newLessonsList, tenantId));

        [Authorize]
        [HttpPut]
        public async Task<ServiceResult<Empty>> UpdateLesson([FromBody] LessonUpdateModel lessonModel)
            => await RunWithServiceResult(async () => await _service.UpdateItem(lessonModel));

        [Authorize]
        [HttpGet("{lessonId}")]
        public async Task<ServiceResult<LessonModel>> GetLesson(Guid lessonId)
            => await RunWithServiceResult(async () => await _service.First(x => x.Id == lessonId));

        [Authorize]
        [HttpDelete("{lessonId}")]
        public async Task<ServiceResult<Empty>> RemoveLesson(Guid lessonId)
            => await RunWithServiceResult(async () => await _service.RemoveItem(lessonId));

        //[Authorize]
        [HttpPost("attendance/{pupilId}")]
        public async Task<ServiceResult<Empty>> TickPupilAttendance(Guid pupilId)
            => await RunWithServiceResult(async () => await _attendanceService.TickPupilAttendance(pupilId));
    }
}
