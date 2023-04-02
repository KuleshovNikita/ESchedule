using ESchedule.Api.Models.Updates;
using ESchedule.Domain;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public interface IBaseService<T> where T : BaseModel
    {
        Task<ServiceResult<Empty>> CreateItem<TCreateModel>(TCreateModel ItemModel);

        Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate);

        Task<ServiceResult<T>> First(Expression<Func<T, bool>> predicate);

        Task<ServiceResult<IEnumerable<T>>> Where(Expression<Func<T, bool>> predicate);

        Task<ServiceResult<Empty>> RemoveItem(Guid itemId);

        Task<ServiceResult<Empty>> UpdateItem<TUpdatedModel>(TUpdatedModel updateModel) where TUpdatedModel : BaseUpdateModel;
    }
}
