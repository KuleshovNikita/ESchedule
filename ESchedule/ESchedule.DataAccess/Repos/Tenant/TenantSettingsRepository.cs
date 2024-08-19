using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Tenant
{
    public class TenantSettingsRepository : Repository<TenantSettingsModel>
    {
        public TenantSettingsRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<TenantSettingsModel> FirstOrDefault(Expression<Func<TenantSettingsModel, bool>> command)
            => await GetContext<TenantSettingsModel>()
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<TenantSettingsModel>> Where(Expression<Func<TenantSettingsModel, bool>> command)
            => await GetContext<TenantSettingsModel>()
                    .Include(x => x.Tenant)
                    .Where(command)
                    .ToListAsync()
                        ?? throw new EntityNotFoundException();
    }
}
