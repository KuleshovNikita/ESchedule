using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<ServiceResult<T>> FirstOrDefault(Expression<Func<T, bool>> command);

        Task<ServiceResult<IEnumerable<T>>> Where(Expression<Func<T, bool>> command);

        Task<ServiceResult<bool>> Any(Expression<Func<T, bool>> command);

        Task<ServiceResult<Empty>> Insert(T entity);

        Task<ServiceResult<Empty>> Update(T entity);

        Task<ServiceResult<Empty>> Remove(T entity);
    }
}
