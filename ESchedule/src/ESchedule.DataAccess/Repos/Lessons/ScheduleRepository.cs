using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons.Schedule;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Lessons
{
    public class ScheduleRepository : Repository<ScheduleModel>
    {
        public ScheduleRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ScheduleModel> FirstOrDefault(Expression<Func<ScheduleModel, bool>> command)
            => await GetContext<ScheduleModel>()
                    .Include(x => x.Tenant)
                    .Include(x => x.Lesson)
                    .Include(x => x.Teacher)
                    .Include(x => x.StudyGroup)
                    .FirstOrDefaultAsync(command)
                        ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<ScheduleModel>> Where(Expression<Func<ScheduleModel, bool>> command)
            => await GetContext<ScheduleModel>()
                    .Include(x => x.Tenant)
                    .Include(x => x.Lesson)
                    .Include(x => x.Teacher)
                    .Include(x => x.StudyGroup)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
