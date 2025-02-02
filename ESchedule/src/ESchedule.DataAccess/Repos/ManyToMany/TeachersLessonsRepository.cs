using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany
{
    public class TeachersLessonsRepository : Repository<TeachersLessonsModel>
    {
        public TeachersLessonsRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<TeachersLessonsModel> FirstOrDefault(Expression<Func<TeachersLessonsModel, bool>> command)
            => await GetContext<TeachersLessonsModel>()
                    .Include(x => x.Teacher)
                    .Include(x => x.Lesson)
                    .FirstOrDefaultAsync(command) 
                        ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<TeachersLessonsModel>> Where(Expression<Func<TeachersLessonsModel, bool>> command)
            => await GetContext<TeachersLessonsModel>()
                    .Include(x => x.Teacher)
                    .Include(x => x.Lesson)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
