using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.User
{
    public class PupilRepository : BaseRepository<PupilModel>, IUserRepository<PupilModel>
    {
        public PupilRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<PupilModel>> FirstOrDefault(Expression<Func<PupilModel, bool>> command)
        {
            var result = new ServiceResult<PupilModel>();

            try
            {
                result.Value = await _context.Set<PupilModel>()
                    .Include(p => p.Group)
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
