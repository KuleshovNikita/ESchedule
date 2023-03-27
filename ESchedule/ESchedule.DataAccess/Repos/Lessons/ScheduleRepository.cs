using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Lessons
{
    public class ScheduleRepository : Repository<ScheduleModel>
    {
        public ScheduleRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<ScheduleModel>> First(Expression<Func<ScheduleModel, bool>> command)
        {
            var result = new ServiceResult<ScheduleModel>();

            try
            {
                result.Value = await _context.Set<ScheduleModel>()
                    .Include(x => x.Tenant)
                    .Include(x => x.Lesson)
                    .Include(x => x.Teacher)
                    .Include(x => x.StudyGroup)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<ScheduleModel>>> Where(Expression<Func<ScheduleModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<ScheduleModel>>();

            try
            {
                result.Value = _context.Set<ScheduleModel>()
                    .Include(x => x.Tenant)
                    .Include(x => x.Lesson)
                    .Include(x => x.Teacher)
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
