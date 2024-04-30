using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly TenantEScheduleDbContext _context;

        public Repository(TenantEScheduleDbContext context) => _context = context;

        public virtual async Task<TModel> FirstOrDefault(Expression<Func<TModel, bool>> command)
            => await _context.Set<TModel>().FirstOrDefaultAsync(command) 
                ?? throw new EntityNotFoundException();

        public virtual async Task<TModel> SingleOrDefault(Expression<Func<TModel, bool>> command)
           => await _context.Set<TModel>().SingleOrDefaultAsync(command);

        public virtual async Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> command)
            => await _context.Set<TModel>().Where(command).ToListAsync() 
                ?? throw new EntityNotFoundException();

        public virtual async Task<IEnumerable<TModel>> All()
            => await _context.Set<TModel>().Where(x => true).ToListAsync();

        public virtual async Task<bool> Any(Expression<Func<TModel, bool>> command)
            => await _context.Set<TModel>().AnyAsync(command);

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
    }
}
