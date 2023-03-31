using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Lessons
{
    public class LessonRepository : Repository<LessonModel>
    {
        public LessonRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<LessonModel>> First(Expression<Func<LessonModel, bool>> command)
        {
            var result = new ServiceResult<LessonModel>();

            try
            {
                result.Value = await _context.Set<LessonModel>()
                    .Include(x => x.StudingGroups)
                    .Include(x => x.ResponsibleTeachers)
                    .Include(x => x.RelatedSchedules)
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<LessonModel>>> Where(Expression<Func<LessonModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<LessonModel>>();

            try
            {
                result.Value = _context.Set<LessonModel>()
                    .Include(x => x.StudingGroups)
                    .Include(x => x.ResponsibleTeachers)
                    .Include(x => x.RelatedSchedules)
                    .Include(x => x.Tenant)
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
