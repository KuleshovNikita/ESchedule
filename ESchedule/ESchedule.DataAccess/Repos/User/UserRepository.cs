using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.User
{
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<UserModel>> First(Expression<Func<UserModel, bool>> command)
        {
            var result = new ServiceResult<UserModel>();

            try
            {
                result.Value = await _context.Set<UserModel>()
                    .Include(x => x.Group)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<UserModel>>> Where(Expression<Func<UserModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<UserModel>>();

            try
            {
                result.Value = _context.Set<UserModel>()
                    .Where(command)
                    .Include(p => p.Group) ?? throw new EntityNotFoundException();

                return Task.FromResult(result.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(result.Fail(ex));
            }
        }
    }
}
