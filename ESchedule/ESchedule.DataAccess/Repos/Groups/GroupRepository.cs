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
        public GroupRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<GroupModel>> First(Expression<Func<GroupModel, bool>> command)
        {
            var result = new ServiceResult<GroupModel>();

            try
            {
                result.Value = await _context.Set<GroupModel>()
                    .Include(x => x.StudingLessons)
                    .Include(x => x.Members)
                    .Include(x => x.GroupTeachersLessons)
                    .Include(x => x.StudySchedules)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<GroupModel>>> Where(Expression<Func<GroupModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<GroupModel>>();

            try
            {
                result.Value = _context.Set<GroupModel>()
                    .Include(x => x.StudingLessons)
                    .Include(x => x.Members)
                    .Include(x => x.GroupTeachersLessons)
                    .Include(x => x.StudySchedules)
                    .Where(command)
                        ?? throw new EntityNotFoundException();

                return Task.FromResult(result.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(result.Fail(ex));
            }
        }
    }
}
