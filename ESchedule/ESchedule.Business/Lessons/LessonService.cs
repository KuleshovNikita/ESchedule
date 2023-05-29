using AutoMapper;
using ESchedule.DataAccess.Context;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;

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

        public async Task<ServiceResult<Empty>> UpdateLessonsList(IEnumerable<Guid> newLessonsList, Guid tenantId)
        {
            var lessons = _context.Lessons
                            .Where(x => x.TenantId == tenantId && !newLessonsList.Contains(x.Id));

            _context.Lessons.RemoveRange(lessons);

            await _context.SaveChangesAsync();

            //var tenantLessons = (await Where(x => x.TenantId == tenantId)).Value;
            //var removedLessons = tenantLessons.Where(x => !newLessonsList.Contains(x.Id)).Select(x => x.Id);
            var result = new ServiceResult<Empty>();

            //foreach (var lesson in removedLessons)
            //{
            //    result = await RemoveItem(lesson);
            //}

            return result;
        }
    }
}
