using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Lessons
{
    public class LessonRepository : Repository<LessonModel>
    {
        public LessonRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<LessonModel> First(Expression<Func<LessonModel, bool>> command)
            => await _context.Set<LessonModel>()
                    .Include(x => x.StudingGroups)
                    .Include(x => x.ResponsibleTeachers)
                    .Include(x => x.RelatedSchedules)
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<LessonModel>> Where(Expression<Func<LessonModel, bool>> command)
            => await _context.Set<LessonModel>()
                    .Include(x => x.StudingGroups)
                    .Include(x => x.ResponsibleTeachers)
                    .Include(x => x.RelatedSchedules)
                    .Include(x => x.Tenant)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
