using ESchedule.Api.Models.Updates;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain;
using ESchedule.Domain.Exceptions;
using PowerInfrastructure.AutoMapper;
using System.Linq.Expressions;

namespace ESchedule.Business;

public class BaseService<T>(IRepository<T> repository, IMainMapper mapper) : IBaseService<T>
    where T : BaseModel
{
    protected readonly IRepository<T> Repository = repository;
    protected readonly IMainMapper Mapper = mapper;

    public async virtual Task<T> CreateItem<TCreateModel>(TCreateModel itemCreateModel)
    {
        var itemDomainModel = Mapper.Map<T>(itemCreateModel);
        // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
        itemDomainModel.Id = Guid.NewGuid();
        return await Repository.Insert(itemDomainModel);
    }

    public async virtual Task InsertMany(IEnumerable<T> itemsSet)
    {
        // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
        await Repository.InsertMany(itemsSet);
    }

    public async virtual Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> itemsSet)
    {
        // тут долна быть валидация, но надо проверить как валидирует модельки апи Model.IsValid, может в бинесе и не придется ничего валидировать
        var mappedItems = Mapper.Map<IEnumerable<T>>(itemsSet);
        await Repository.InsertMany(mappedItems);
    }

    public async virtual Task<IEnumerable<T>> GetItems(Expression<Func<T, bool>> predicate, bool includeNavs = false)
        => await Repository.Where(predicate);

    public async virtual Task<IEnumerable<T>> GetItems()
        => await Repository.All();

    public async virtual Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        => await Repository.FirstOrDefault(predicate);

    public async virtual Task<T> SingleOrDefault(Expression<Func<T, bool>> predicate)
        => await Repository.SingleOrDefault(predicate);

    public async virtual Task<T> SingleOrDefault()
        => await Repository.SingleOrDefault();

    public async virtual Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        => await Repository.Where(predicate);

    public async virtual Task RemoveItem(Guid itemId)
    {
        await ThrowIfDoesNotExist(itemId);

        var item = await SingleOrDefault(x => x.Id == itemId);
        await Repository.Remove(item);
    }

    public async virtual Task RemoveItem(T item)
    {
        await ThrowIfDoesNotExist(item.Id);

        await Repository.Remove(item);
    }

    public async virtual Task UpdateItem<TUpdatedModel>(TUpdatedModel updateModel)
        where TUpdatedModel : BaseUpdateModel
    {
        await ThrowIfDoesNotExist(updateModel.Id);

        var user = await FirstOrDefault(x => x.Id == updateModel.Id);
        user = Mapper.MapOnlyUpdatedProperties(updateModel, user);

        await Repository.Update(user);
    }

    protected async Task<bool> ItemExists(Guid itemId)
        => await Repository.Any(x => x.Id == itemId);

    protected async Task ThrowIfDoesNotExist(Guid itemId, string errorMessage = null!)
    {
        if (!await ItemExists(itemId))
        {
            throw new EntityNotFoundException(errorMessage);
        }
    }
}