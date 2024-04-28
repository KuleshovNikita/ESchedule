using AutoMapper;
using ESchedule.Api.Models.Updates;
using ESchedule.Business.Extensions;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Tenant;
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

        public async virtual Task<T> CreateItem<TCreateModel>(TCreateModel itemCreateModel)
        {
            var itemDomainModel = _mapper.Map<T>(itemCreateModel);
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            itemDomainModel.Id = Guid.NewGuid();
            return await _repository.Insert(itemDomainModel);
        }

        public async virtual Task InsertMany(IEnumerable<T> itemsSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            await _repository.InsertMany(itemsSet);
        }

        public async virtual Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> itemsSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            var mappedItems = _mapper.Map<IEnumerable<T>>(itemsSet);
            await _repository.InsertMany(mappedItems);
        }

        public async virtual Task<IEnumerable<T>> GetItems(Expression<Func<T, bool>> predicate, bool includeNavs = false)
            => await _repository.Where(predicate);

        public async virtual Task<T> First(Expression<Func<T, bool>> predicate)
            => await _repository.First(predicate);

        public async virtual Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
            => await _repository.Where(predicate);

        public async virtual Task RemoveItem(Guid itemId)
        {
            if (await ItemExists(itemId))
            {
                var items = await GetItems(x => x.Id == itemId);
                await _repository.Remove(items.First());
            }

            throw new EntityNotFoundException();
        }

        public async virtual Task RemoveItem(T item)
        {
            if (await ItemExists(item.Id))
            {
                await _repository.Remove(item);
            }

            throw new EntityNotFoundException();
        }

        public async virtual Task UpdateItem<TUpdatedModel>(TUpdatedModel updateModel)
            where TUpdatedModel : BaseUpdateModel
        {
            if (!await ItemExists(updateModel.Id))
            {
                throw new EntityNotFoundException();
            }

            var user = await First(x => x.Id == updateModel.Id);
            user = _mapper.MapOnlyUpdatedProperties(updateModel, user);

            await _repository.Update(user);
        }

        protected async Task<bool> ItemExists(Guid itemId)
            => await _repository.Any(x => x.Id == itemId);
    }
}
