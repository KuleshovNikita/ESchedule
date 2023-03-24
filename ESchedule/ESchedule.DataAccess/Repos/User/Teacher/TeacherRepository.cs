using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.User.Teacher
{
    public class TeacherRepository : BaseRepository<TeacherModel>, ITeacherRepository
    {
        public TeacherRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<TeacherModel>> FirstOrDefault(Expression<Func<TeacherModel, bool>> command)
        {
            var result = new ServiceResult<TeacherModel>();

            try
            {
                result.Value = await _context.Set<TeacherModel>()
                    .Include(t => t.OwnGroup)
                    .Include(t => t.StudyGroups)
                    .Include(t => t.StudySchedules)
                    .Include(t => t.TaughtLessons)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }
    }
}
