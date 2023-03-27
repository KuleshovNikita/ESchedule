using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EScheduleDbContext _context;

        public Repository(EScheduleDbContext context) => _context = context;

        public virtual async Task<ServiceResult<T>> First(Expression<Func<T, bool>> command)
        {
            var result = new ServiceResult<T>();

            try
            {
                result.Value = await _context.Set<T>().FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual Task<ServiceResult<IEnumerable<T>>> Where(Expression<Func<T, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<T>>();

            try
            {
                result.Value = _context.Set<T>().Where(command) ?? throw new EntityNotFoundException();

                return Task.FromResult(result.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(result.Fail(ex));
            }
        }

        public virtual async Task<ServiceResult<bool>> Any(Expression<Func<T, bool>> command)
        {
            var result = new ServiceResult<bool>();

            try
            {
                result.Value = await _context.Set<T>().AnyAsync(command);

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Insert(T entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Update(T entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                _context.Set<T>().Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Remove(T entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }
    }
}
