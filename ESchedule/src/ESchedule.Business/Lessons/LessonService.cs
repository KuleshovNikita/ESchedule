using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Lessons;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.Lessons;

public class LessonService(
    IRepository<LessonModel> repository, 
    IMainMapper mapper, 
    EScheduleDbContext dbContext
) 
    : BaseService<LessonModel>(repository, mapper), ILessonService
{
    private readonly EScheduleDbContext _context = dbContext;

    public async Task RemoveLessons(IEnumerable<Guid> lessonsToRemove)
    {
        var lessons = _context.Lessons.Where(x => lessonsToRemove.Contains(x.Id));

        _context.Lessons.RemoveRange(lessons);

        await _context.SaveChangesAsync();

        //var tenantLessons = (await Where(x => x.TenantId == tenantId)).Value;
        //var removedLessons = tenantLessons.Where(x => !newLessonsList.Contains(x.Id)).Select(x => x.Id);

        //foreach (var lesson in removedLessons)
        //{
        //    result = await RemoveItem(lesson);
        //}
    }
}