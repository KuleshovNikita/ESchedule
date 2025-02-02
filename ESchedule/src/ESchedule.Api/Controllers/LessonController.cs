using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Updates;
using ESchedule.Business;
using ESchedule.Business.Lessons;
using ESchedule.Domain.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers
{
    public class LessonController : BaseController<LessonModel>
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
        public async Task<LessonModel> CreateLesson([FromBody] LessonCreateModel lessonModel) 
            => await _service.CreateItem(lessonModel);

        [Authorize]
        [HttpPut("many")]
        public async Task RemoveLessons([FromBody] IEnumerable<Guid> lessonsToRemove)
            => await _lessonService.RemoveLessons(lessonsToRemove);

        [Authorize]
        [HttpPut]
        public async Task UpdateLesson([FromBody] LessonUpdateModel lessonModel)
            => await _service.UpdateItem(lessonModel);

        [Authorize]
        [HttpGet("{lessonId}")]
        public async Task<LessonModel> GetLesson(Guid lessonId)
            => await _service.FirstOrDefault(x => x.Id == lessonId);

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<LessonModel>> GetLessons()
            => await _service.GetItems();

        [Authorize]
        [HttpDelete("{lessonId}")]
        public async Task RemoveLesson(Guid lessonId)
            => await _service.RemoveItem(lessonId);

        //[Authorize]
        [HttpPost("attendance/{pupilId}")]
        public async Task TickPupilAttendance(Guid pupilId)
            => await _attendanceService.TickPupilAttendance(pupilId);
    }
}
