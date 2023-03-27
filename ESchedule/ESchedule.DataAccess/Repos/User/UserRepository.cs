using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.User
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<UserModel>> FirstOrDefault(Expression<Func<UserModel, bool>> command)
        {
            var result = new ServiceResult<UserModel>();

            try
            {
                result.Value = await _context.Set<UserModel>()
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
