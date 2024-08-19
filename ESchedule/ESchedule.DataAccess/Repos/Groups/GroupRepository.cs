using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Groups
{
    public class GroupRepository : Repository<GroupModel>
    {
        public GroupRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<GroupModel> FirstOrDefault(Expression<Func<GroupModel, bool>> command)
            => await GetContext<GroupModel>()
                    .Include(x => x.StudingLessons)
                    .Include(x => x.Members)
                    .Include(x => x.GroupTeachersLessons)
                    .Include(x => x.StudySchedules)
                    .FirstOrDefaultAsync(command) 
                        ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<GroupModel>> Where(Expression<Func<GroupModel, bool>> command)
            => await GetContext<GroupModel>()
                    .Include(x => x.StudingLessons)
                    .Include(x => x.Members)
                    .Include(x => x.GroupTeachersLessons)
                    .Include(x => x.StudySchedules)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
