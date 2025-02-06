using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.User;

public class UserRepository(TenantEScheduleDbContext context) : Repository<UserModel>(context)
{
    public override async Task<UserModel> FirstOrDefault(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .Include(x => x.Group)
                .Include(x => x.Tenant)
                .Include(x => x.StudyGroups)
                .Include(x => x.StudySchedules)
                .Include(x => x.TaughtLessons)
                .FirstOrDefaultAsync(command)
                    ?? throw new EntityNotFoundException();

    public override async Task<UserModel> SingleOrDefault(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .Include(x => x.Group)
                .Include(x => x.Tenant)
                .Include(x => x.StudyGroups)
                .Include(x => x.StudySchedules)
                .Include(x => x.TaughtLessons)
                .SingleOrDefaultAsync(command);

    public override async Task<IEnumerable<UserModel>> Where(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .Include(x => x.Group)
                .Include(x => x.Tenant)
                .Include(x => x.StudyGroups)
                .Include(x => x.StudySchedules)
                .Include(x => x.TaughtLessons)
                .Where(command)
                .ToListAsync()
                    ?? throw new EntityNotFoundException();
}
