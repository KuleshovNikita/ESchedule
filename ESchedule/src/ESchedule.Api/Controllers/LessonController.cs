using ESchedule.Api.Models.Requests.Create.Lessons;
using ESchedule.Api.Models.Requests.Update.Lessons;
using ESchedule.Business;
using ESchedule.Business.Lessons;
using ESchedule.Domain.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchedule.Api.Controllers;

public class LessonController(
    IBaseService<LessonModel> baseLessonService, 
    ILessonService lessonService,
    IAttendanceService attendanceService
) 
    : BaseController<LessonModel>(baseLessonService)
{
    [Authorize]
    [HttpPost]
    public async Task<LessonModel> CreateLesson([FromBody] LessonCreateModel lessonModel) 
        => await service.CreateItem(lessonModel);

    [Authorize]
    [HttpPut("many")]
    public async Task RemoveLessons([FromBody] IEnumerable<Guid> lessonsToRemove)
        => await lessonService.RemoveLessons(lessonsToRemove);

    [Authorize]
    [HttpPut]
    public async Task UpdateLesson([FromBody] LessonUpdateModel lessonModel)
        => await service.UpdateItem(lessonModel);

    [Authorize]
    [HttpGet("{lessonId}")]
    public async Task<LessonModel> GetLesson(Guid lessonId)
        => await service.FirstOrDefault(x => x.Id == lessonId);

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<LessonModel>> GetLessons()
        => await service.GetItems();

    [Authorize]
    [HttpDelete("{lessonId}")]
    public async Task RemoveLesson(Guid lessonId)
        => await service.RemoveItem(lessonId);

    //[Authorize]
    [HttpPost("attendance/{pupilId}")]
    public async Task TickPupilAttendance(Guid pupilId)
        => await attendanceService.TickPupilAttendance(pupilId);
}
