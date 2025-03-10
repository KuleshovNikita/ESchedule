﻿using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos;

public interface IRepository<TModel> where TModel : class
{
    Task<TModel> FirstOrDefault(Expression<Func<TModel, bool>> command);

    Task<TModel> SingleOrDefault(Expression<Func<TModel, bool>> command);

    Task<TModel> SingleOrDefault();

    Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> command);

    Task<IEnumerable<TModel>> All();

    Task<bool> Any(Expression<Func<TModel, bool>> command);

    Task<TModel> Insert(TModel entity);

    Task InsertMany(IEnumerable<TModel> entities);

    Task Update(TModel entity);

    Task Remove(TModel entity);

    Task RemoveRange(IEnumerable<TModel> entities);

    Task SaveChangesAsync();

    IRepository<TModel> IgnoreQueryFilters();
}
