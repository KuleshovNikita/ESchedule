using ESchedule.Api.Models.Updates;
using ESchedule.Domain;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<ServiceResult<Empty>> CreateItem<TCreateModel>(TCreateModel itemModel);

        Task<ServiceResult<Empty>> InsertMany(IEnumerable<T> itemsSet);

        Task<ServiceResult<Empty>> InsertMany<K>(IEnumerable<K> itemsSet);

        Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate, bool includeNavs = false);

        Task<ServiceResult<T>> First(Expression<Func<T, bool>> predicate);
        Task<T> FirstNew(Expression<Func<T, bool>> predicate) => throw new Exception();

        Task<ServiceResult<IEnumerable<T>>> Where(Expression<Func<T, bool>> predicate);

        Task<ServiceResult<Empty>> RemoveItem(Guid itemId);

        Task<ServiceResult<Empty>> RemoveItem(T item);

        Task<ServiceResult<Empty>> UpdateItem<TUpdatedModel>(TUpdatedModel updateModel) where TUpdatedModel : BaseUpdateModel;
    }
}
