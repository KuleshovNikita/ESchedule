using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Tenant
{
    public class TenantRepository : Repository<TenantModel>
    {
        public TenantRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<TenantModel> First(Expression<Func<TenantModel, bool>> command)
            => await _context.Set<TenantModel>()
                    .Include(x => x.Settings)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<TenantModel>> Where(Expression<Func<TenantModel, bool>> command)
            => await _context.Set<TenantModel>()
                    .Include(x => x.Settings)
                    .Where(command)
                    .ToListAsync()  
                        ?? throw new EntityNotFoundException();
    }
}
