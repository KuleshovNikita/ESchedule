using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.ServiceResulting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly EScheduleDbContext _context;

        public Repository(EScheduleDbContext context) => _context = context;

        public virtual async Task<ServiceResult<TModel>> First(Expression<Func<TModel, bool>> command)
        {
            var result = new ServiceResult<TModel>();

            try
            {
                result.Value = await _context.Set<TModel>().FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<TModel> FirstNew(Expression<Func<TModel, bool>> command) => throw new Exception();

        public virtual Task<ServiceResult<IEnumerable<TModel>>> Where(Expression<Func<TModel, bool>> command)
        {
            var result = new ServiceResult<IEnumerable<TModel>>();

            try
            {
                result.Value = _context.Set<TModel>().Where(command) ?? throw new EntityNotFoundException();

                return Task.FromResult(result.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(result.Fail(ex));
            }
        }

        public virtual async Task<ServiceResult<bool>> Any(Expression<Func<TModel, bool>> command)
        {
            var result = new ServiceResult<bool>();

            try
            {
                result.Value = await _context.Set<TModel>().AnyAsync(command);

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Insert(TModel entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                await _context.Set<TModel>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> InsertMany(IEnumerable<TModel> entities)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                await _context.Set<TModel>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Update(TModel entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                _context.Set<TModel>().Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> Remove(TModel entity)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                _context.Set<TModel>().Remove(entity);
                await _context.SaveChangesAsync();

                return result.Success();
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public virtual async Task<ServiceResult<Empty>> RemoveRange(IEnumerable<TModel> entities)
        {
            var result = new ServiceResult<Empty>();

            try
            {
                _context.Set<TModel>().RemoveRange(entities);
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
