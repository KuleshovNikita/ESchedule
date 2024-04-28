using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Auth
{
    public class AuthRepository : Repository<UserModel>, IAuthRepository
    {
        public AuthRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<UserModel> First(Expression<Func<UserModel, bool>> command)
            => await _context.Set<UserModel>()
                    .IgnoreQueryFilters()
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command)
                        ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<UserModel>> Where(Expression<Func<UserModel, bool>> command)
            => await _context.Set<UserModel>()
                    .IgnoreQueryFilters()   
                    .Include(x => x.Tenant)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
