﻿using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Auth;

public class AuthRepository(TenantEScheduleDbContext context) : Repository<UserModel>(context), IAuthRepository
{
    public override async Task<UserModel> FirstOrDefault(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .IgnoreQueryFilters()
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(command)
                    ?? throw new EntityNotFoundException();

    public override async Task<IEnumerable<UserModel>> Where(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .IgnoreQueryFilters()   
                .Include(x => x.Tenant)
                .Where(command)
                .ToListAsync()
                    ?? throw new EntityNotFoundException();

    public override async Task<bool> Any(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .IgnoreQueryFilters()
                .AnyAsync(command);

    public override async Task<UserModel> SingleOrDefault(Expression<Func<UserModel, bool>> command)
        => await GetContext<UserModel>()
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(command);
}
