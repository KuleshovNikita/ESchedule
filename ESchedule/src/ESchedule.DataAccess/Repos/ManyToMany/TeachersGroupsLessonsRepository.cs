using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany;

public class TeachersGroupsLessonsRepository(TenantEScheduleDbContext context) : Repository<TeachersGroupsLessonsModel>(context)
{
    public override async Task<TeachersGroupsLessonsModel> FirstOrDefault(Expression<Func<TeachersGroupsLessonsModel, bool>> command)
        => await GetContext<TeachersGroupsLessonsModel>()
                .Include(x => x.Teacher)
                .Include(x => x.Lesson)
                .Include(x => x.StudyGroup)
                .FirstOrDefaultAsync(command)
                    ?? throw new EntityNotFoundException();

    public override async Task<IEnumerable<TeachersGroupsLessonsModel>> Where(Expression<Func<TeachersGroupsLessonsModel, bool>> command) 
        => await GetContext<TeachersGroupsLessonsModel>()
                .Include(x => x.Teacher)
                .Include(x => x.Lesson)
                .Include(x => x.StudyGroup)
                .Where(command)
                .ToListAsync() 
                    ?? throw new EntityNotFoundException();
}
