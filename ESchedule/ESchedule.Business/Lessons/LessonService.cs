using AutoMapper;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Lessons;

namespace ESchedule.Business.Lessons
{
    public class LessonService : BaseService<LessonModel>, ILessonService
    {
        private readonly EScheduleDbContext _context;

        public LessonService(IRepository<LessonModel> repository, IMapper mapper, EScheduleDbContext dbContext) 
            : base(repository, mapper)
        {
            _context = dbContext;
        }

        public async Task RemoveLessons(IEnumerable<Guid> lessonsToRemove, Guid tenantId)
        {
            var lessons = _context.Lessons
                            .Where(x => x.TenantId == tenantId && lessonsToRemove.Contains(x.Id));

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
