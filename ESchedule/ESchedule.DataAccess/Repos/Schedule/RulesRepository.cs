using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Schedule
{
    public class RulesRepository : Repository<RuleModel>
    {
        public RulesRepository(EScheduleDbContext context) : base(context)
        {
        }

        public override async Task<ServiceResult<RuleModel>> First(Expression<Func<RuleModel, bool>> command)
        {
            var result = new ServiceResult<RuleModel>();

            try
            {
                result.Value = await _context.Set<RuleModel>()
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public override Task<ServiceResult<IEnumerable<RuleModel>>> Where(Expression<Func<RuleModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<RuleModel>>();

            try
            {
                result.Value = _context.Set<RuleModel>()
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
