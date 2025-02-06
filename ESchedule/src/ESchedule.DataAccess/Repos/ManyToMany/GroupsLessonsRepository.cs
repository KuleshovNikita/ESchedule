using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany;

public class GroupsLessonsRepository(TenantEScheduleDbContext context) : Repository<GroupsLessonsModel>(context)
{
    public override async Task<GroupsLessonsModel> FirstOrDefault(Expression<Func<GroupsLessonsModel, bool>> command)
        => await GetContext<GroupsLessonsModel>()
                .Include(x => x.StudyGroup)
                .Include(x => x.Lesson)
                .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();
    public override async Task<IEnumerable<GroupsLessonsModel>> Where(Expression<Func<GroupsLessonsModel, bool>> command)
        => await GetContext<GroupsLessonsModel>()
                .Include(x => x.StudyGroup)
                .Include(x => x.Lesson)
                .Where(command)
                .ToListAsync()
                    ?? throw new EntityNotFoundException();
}
