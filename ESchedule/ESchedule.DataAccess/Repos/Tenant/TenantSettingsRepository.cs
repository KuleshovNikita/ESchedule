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
        public TenantSettingsRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<TenantSettingsModel>> First(Expression<Func<TenantSettingsModel, bool>> command)
        {
            var result = new ServiceResult<TenantSettingsModel>();

            try
            {
                result.Value = await _context.Set<TenantSettingsModel>()
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<TenantSettingsModel>>> Where(Expression<Func<TenantSettingsModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<TenantSettingsModel>>();

            try
            {
                result.Value = _context.Set<TenantSettingsModel>()
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
