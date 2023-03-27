using AutoMapper;
using ESchedule.Business.Extensions;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Properties;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public class BaseService<T> where T : BaseModel
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async virtual Task<ServiceResult<Empty>> CreateItem(T ItemModel)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            ItemModel.Id = Guid.NewGuid();
            return (await _repository.Insert(ItemModel)).Success();
        }

        public async virtual Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate)
            => (await _repository.Where(predicate)).Success();

        public async virtual Task<ServiceResult<Empty>> RemoveItem(Guid itemId)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (await ItemExists(itemId))
            {
                var items = await GetItems(x => x.Id == itemId);
                return (await _repository.Remove(items.Value.First())).Success();
            }

            return serviceResult.FailAndThrow(Resources.TheItemDoesntExist);
        }

        public async virtual Task<ServiceResult<Empty>> UpdateItem<K>(K updateModel, Guid itemId)
            where K : BaseModel
        {
            var serviceResult = new ServiceResult<Empty>();

            if (!await ItemExists(itemId))
            {
                return serviceResult.FailAndThrow(Resources.TheItemDoesntExist);
            }

            updateModel.Id = itemId;
            var user = (await GetItems(x => x.Id == updateModel.Id)).Value.First();
            user = _mapper.MapOnlyUpdatedProperties(updateModel, user);

            var result = await _repository.Update(user);
            return result.Success();
        }

        protected async Task<bool> ItemExists(Guid itemId)
        {
            var result = await _repository.Any(x => x.Id == itemId);
            return result.CatchAny().Value;
        }
    }
}
