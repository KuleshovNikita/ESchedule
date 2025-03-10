﻿using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Tenant;

public class TenantRepository(TenantEScheduleDbContext context) : Repository<TenantModel>(context)
{
    public override async Task<TenantModel> FirstOrDefault(Expression<Func<TenantModel, bool>> command)
        => await GetContext<TenantModel>()
                .IgnoreQueryFilters()
                .Include(x => x.Settings)
                .FirstOrDefaultAsync(command);

    public override async Task<TenantModel> SingleOrDefault(Expression<Func<TenantModel, bool>> command)
        => await GetContext<TenantModel>()
                .IgnoreQueryFilters()
                .Include(x => x.Settings)
                .SingleOrDefaultAsync(command);


    public override async Task<bool> Any(Expression<Func<TenantModel, bool>> command)
        => await GetContext<TenantModel>()
                .IgnoreQueryFilters()
                .AnyAsync(command);

    public override async Task<IEnumerable<TenantModel>> Where(Expression<Func<TenantModel, bool>> command)
        => await GetContext<TenantModel>()
                .IgnoreQueryFilters()   
                .Include(x => x.Settings)
                .Where(command)
                .ToListAsync()  
                    ?? throw new EntityNotFoundException();
}
