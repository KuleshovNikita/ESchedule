using ESchedule.Api.Models.Updates;
using ESchedule.Domain;
using System.Linq.Expressions;

namespace ESchedule.Business;

public interface IBaseService<TModel> where TModel : BaseModel
{
    Task<TModel> CreateItem<TCreateModel>(TCreateModel itemModel);

    Task InsertMany(IEnumerable<TModel> itemsSet);

    Task InsertMany<K>(IEnumerable<K> itemsSet);

    Task<IEnumerable<TModel>> GetItems(Expression<Func<TModel, bool>> predicate, bool includeNavs = false);

    Task<IEnumerable<TModel>> GetItems();

    Task<TModel> FirstOrDefault(Expression<Func<TModel, bool>> predicate) => throw new Exception();

    Task<TModel> SingleOrDefault(Expression<Func<TModel, bool>> predicate);

    Task<TModel> SingleOrDefault();

    Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> predicate);

    Task RemoveItem(Guid itemId);

    Task RemoveItem(TModel item);

    Task UpdateItem<TUpdatedModel>(TUpdatedModel updateModel) where TUpdatedModel : BaseUpdateModel;
}
