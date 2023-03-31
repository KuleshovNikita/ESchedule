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

        public override async Task<ServiceResult<TenantModel>> First(Expression<Func<TenantModel, bool>> command)
        {
            var result = new ServiceResult<TenantModel>();

            try
            {
                result.Value = await _context.Set<TenantModel>()
                    .Include(x => x.Settings)
                    .Include(x => x.Creator)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<TenantModel>>> Where(Expression<Func<TenantModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<TenantModel>>();

            try
            {
                result.Value = _context.Set<TenantModel>()
                    .Include(x => x.Settings)
                    .Include(x => x.Creator)
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
