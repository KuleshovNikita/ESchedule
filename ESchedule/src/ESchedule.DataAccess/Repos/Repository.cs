using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos;

public class Repository<TModel>(TenantEScheduleDbContext context) : IRepository<TModel> where TModel : class
{
    protected readonly TenantEScheduleDbContext _context = context;
    private bool _ignoreQueryFilters = false;

    public virtual async Task<TModel> FirstOrDefault(Expression<Func<TModel, bool>> command)
        => await GetContext<TModel>().FirstOrDefaultAsync(command)
            ?? throw new EntityNotFoundException();

    public virtual async Task<TModel> SingleOrDefault(Expression<Func<TModel, bool>> command)
       => await GetContext<TModel>().SingleOrDefaultAsync(command);
    
    public virtual async Task<TModel> SingleOrDefault()
       => await GetContext<TModel>().SingleOrDefaultAsync();

    public virtual async Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> command)
        => await GetContext<TModel>().Where(command).ToListAsync() 
            ?? throw new EntityNotFoundException();

    public virtual async Task<IEnumerable<TModel>> All()
        => await GetContext<TModel>().Where(x => true).ToListAsync();

    public virtual async Task<bool> Any(Expression<Func<TModel, bool>> command)
        => await GetContext<TModel>().AnyAsync(command);

    public virtual async Task<TModel> Insert(TModel entity)
    {
        var result = await _context.Set<TModel>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public virtual async Task InsertMany(IEnumerable<TModel> entities)
    {
        await _context.Set<TModel>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(TModel entity)
    {
        _context.Set<TModel>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task Remove(TModel entity)
    {
        _context.Set<TModel>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task RemoveRange(IEnumerable<TModel> entities)
    {
        _context.Set<TModel>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();

    public IRepository<TModel> IgnoreQueryFilters()
    {
        _ignoreQueryFilters = true;

        return this;
    }

    protected IQueryable<TEntity> GetContext<TEntity>()
        where TEntity: class
    {
        if(_ignoreQueryFilters)
        {
            return _context.Set<TEntity>().IgnoreQueryFilters();
        }

        return _context.Set<TEntity>().AsQueryable();
    }
}
