using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<ServiceResult<TModel>> First(Expression<Func<TModel, bool>> command);

        Task<ServiceResult<IEnumerable<TModel>>> Where(Expression<Func<TModel, bool>> command);

        Task<ServiceResult<bool>> Any(Expression<Func<TModel, bool>> command);

        Task<ServiceResult<Empty>> Insert(TModel entity);

        Task<ServiceResult<Empty>> InsertMany(IEnumerable<TModel> entities);

        Task<ServiceResult<Empty>> Update(TModel entity);

        Task<ServiceResult<Empty>> Remove(TModel entity);

        Task<ServiceResult<Empty>> RemoveRange(IEnumerable<TModel> entities);
    }
}
