using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public interface IBaseService<T> where T : class
    {
        Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate);

        Task<ServiceResult<Empty>> RemoveItem(Guid itemId);
    }
}
