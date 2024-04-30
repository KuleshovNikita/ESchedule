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
        public RulesRepository(TenantEScheduleDbContext context) : base(context)
        {
        }

        public override async Task<RuleModel> FirstOrDefault(Expression<Func<RuleModel, bool>> command)
            => await _context.Set<RuleModel>()
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(command) 
                        ?? throw new EntityNotFoundException();

        public override async Task<IEnumerable<RuleModel>> Where(Expression<Func<RuleModel, bool>> command) 
            => await _context.Set<RuleModel>()
                    .Include(x => x.Tenant)
                    .Where(command)
                    .ToListAsync() 
                        ?? throw new EntityNotFoundException();
    }
}
