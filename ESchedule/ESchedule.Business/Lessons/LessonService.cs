using AutoMapper;
using ESchedule.Business.Mappers;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Lessons;

namespace ESchedule.Business.Lessons
{
    public class LessonService : BaseService<LessonModel>, ILessonService
    {
        private readonly EScheduleDbContext _context;

        public LessonService(IRepository<LessonModel> repository, IMainMapper mapper, EScheduleDbContext dbContext) 
            : base(repository, mapper)
        {
            _context = dbContext;
        }

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
}
