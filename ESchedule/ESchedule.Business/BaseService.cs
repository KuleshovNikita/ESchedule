using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Properties;
using ESchedule.ServiceResulting;
using System.Linq.Expressions;

namespace ESchedule.Business
{
    public class BaseService<T> : IBaseService<T>
        where T : BaseModel
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async virtual Task<ServiceResult<Empty>> CreateItem<TCreateModel>(TCreateModel ItemCreateModel)
        {
            var itemDomainModel = _mapper.Map<T>(ItemCreateModel);
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            itemDomainModel.Id = Guid.NewGuid();
            return (await _repository.Insert(itemDomainModel)).Success();
        }

        public async virtual Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate)
            => (await _repository.Where(predicate)).Success();

        public async virtual Task<ServiceResult<T>> First(Expression<Func<T, bool>> predicate)
            => (await _repository.First(predicate)).Success();

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

        public async virtual Task<ServiceResult<Empty>> UpdateItem<TUpdatedModel>(TUpdatedModel updateModel)
            where TUpdatedModel : BaseUpdateModel
        {
            var serviceResult = new ServiceResult<Empty>();

            if (!await ItemExists(updateModel.Id))
            {
                return serviceResult.FailAndThrow(Resources.TheItemDoesntExist);
            }

            var user = await First(x => x.Id == updateModel.Id);
            user.Value = _mapper.MapOnlyUpdatedProperties(updateModel, user.Value);

            var result = await _repository.Update(user.Value);
            return result.Success();
        }

        protected async Task<bool> ItemExists(Guid itemId)
        {
            var result = await _repository.Any(x => x.Id == itemId);
            return result.Success().Value;
        }
    }
}
