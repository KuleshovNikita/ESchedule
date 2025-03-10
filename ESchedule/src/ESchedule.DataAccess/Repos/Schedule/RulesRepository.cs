﻿using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Schedule.Rules;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Schedule;

public class RulesRepository(TenantEScheduleDbContext context) : Repository<RuleModel>(context)
{
    public override async Task<RuleModel> FirstOrDefault(Expression<Func<RuleModel, bool>> command)
        => await GetContext<RuleModel>()
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(command) 
                    ?? throw new EntityNotFoundException();

    public override async Task<IEnumerable<RuleModel>> Where(Expression<Func<RuleModel, bool>> command) 
        => await GetContext<RuleModel>()
                .Include(x => x.Tenant)
                .Where(command)
                .ToListAsync() 
                    ?? throw new EntityNotFoundException();
}
