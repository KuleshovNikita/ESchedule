using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.ManyToMany
{
    public class TeachersLessonsRepository : Repository<TeachersLessonsModel>
    {
        public TeachersLessonsRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<TeachersLessonsModel>> First(Expression<Func<TeachersLessonsModel, bool>> command)
        {
            var result = new ServiceResult<TeachersLessonsModel>();

            try
            {
                result.Value = await _context.Set<TeachersLessonsModel>()
                    .Include(x => x.Teacher)
                    .Include(x => x.Lesson)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<TeachersLessonsModel>>> Where(Expression<Func<TeachersLessonsModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<TeachersLessonsModel>>();

            try
            {
                result.Value = _context.Set<TeachersLessonsModel>()
                    .Include(x => x.Teacher)
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
