using ESchedule.Api.Models.Updates;
using ESchedule.Domain;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task CreateItem<TCreateModel>(TCreateModel itemModel);

        Task InsertMany(IEnumerable<T> itemsSet);

        Task InsertMany<K>(IEnumerable<K> itemsSet);

        Task<IEnumerable<T>> GetItems(Expression<Func<T, bool>> predicate, bool includeNavs = false);

        Task<T> First(Expression<Func<T, bool>> predicate) => throw new Exception();

        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);

        Task RemoveItem(Guid itemId);

        Task RemoveItem(T item);

        Task UpdateItem<TUpdatedModel>(TUpdatedModel updateModel) where TUpdatedModel : BaseUpdateModel;
    }
}
