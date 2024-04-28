using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany
{
    public class GroupsLessonsRepository : Repository<GroupsLessonsModel>
    {
        public GroupsLessonsRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<GroupsLessonsModel> First(Expression<Func<GroupsLessonsModel, bool>> command)
            => await _context.Set<GroupsLessonsModel>()
                    .Include(x => x.StudyGroup)
                    .Include(x => x.Lesson)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();
        public override async Task<IEnumerable<GroupsLessonsModel>> Where(Expression<Func<GroupsLessonsModel, bool>> command)
            => await _context.Set<GroupsLessonsModel>()
                    .Include(x => x.StudyGroup)
                    .Include(x => x.Lesson)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
