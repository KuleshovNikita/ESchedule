using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany
{
    public class TeachersGroupsLessonsRepository : Repository<TeachersGroupsLessonsModel>
    {
        public TeachersGroupsLessonsRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<TeachersGroupsLessonsModel>> First(Expression<Func<TeachersGroupsLessonsModel, bool>> command)
        {
            var result = new ServiceResult<TeachersGroupsLessonsModel>();

            try
            {
                result.Value = await _context.Set<TeachersGroupsLessonsModel>()
                    .Include(x => x.Teacher)
                    .Include(x => x.Lesson)
                    .Include(x => x.StudyGroup)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<TeachersGroupsLessonsModel>>> Where(Expression<Func<TeachersGroupsLessonsModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<TeachersGroupsLessonsModel>>();

            try
            {
                result.Value = _context.Set<TeachersGroupsLessonsModel>()
                    .Include(x => x.Teacher)
                    .Include(x => x.Lesson)
                    .Include(x => x.StudyGroup)
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
