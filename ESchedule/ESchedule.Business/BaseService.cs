﻿using AutoMapper;
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

        public async virtual Task<ServiceResult<Empty>> CreateItem<TCreateModel>(TCreateModel itemCreateModel)
        {
            var itemDomainModel = _mapper.Map<T>(itemCreateModel);
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            itemDomainModel.Id = Guid.NewGuid();
            return (await _repository.Insert(itemDomainModel)).Success();
        }

        public async virtual Task<ServiceResult<Empty>> InsertMany(IEnumerable<T> itemsSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            return (await _repository.InsertMany(itemsSet)).Success();
        }

        public async virtual Task<ServiceResult<Empty>> InsertMany<K>(IEnumerable<K> itemsSet)
        {
            // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
            var mappedItems = _mapper.Map<IEnumerable<T>>(itemsSet);
            return (await _repository.InsertMany(mappedItems)).Success();
        }

        public async virtual Task<ServiceResult<IEnumerable<T>>> GetItems(Expression<Func<T, bool>> predicate, bool includeNavs = false)
            => (await _repository.Where(predicate)).Success();

        public async virtual Task<ServiceResult<T>> First(Expression<Func<T, bool>> predicate)
            => (await _repository.First(predicate)).Success();

        public async virtual Task<ServiceResult<IEnumerable<T>>> Where(Expression<Func<T, bool>> predicate)
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

        public async virtual Task<ServiceResult<Empty>> RemoveItem(T item)
        {
            var serviceResult = new ServiceResult<Empty>();

            if (await ItemExists(item.Id))
            {
                return (await _repository.Remove(item)).Success();
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
