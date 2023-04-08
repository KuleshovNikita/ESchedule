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
        public GroupsLessonsRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<GroupsLessonsModel>> First(Expression<Func<GroupsLessonsModel, bool>> command)
        {
            var result = new ServiceResult<GroupsLessonsModel>();

            try
            {
                result.Value = await _context.Set<GroupsLessonsModel>()
                    .Include(x => x.StudyGroup)
                    .Include(x => x.Lesson)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<GroupsLessonsModel>>> Where(Expression<Func<GroupsLessonsModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<GroupsLessonsModel>>();

            try
            {
                result.Value = _context.Set<GroupsLessonsModel>()
                    .Include(x => x.StudyGroup)
                    .Include(x => x.Lesson)
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
