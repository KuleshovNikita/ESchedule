using ESchedule.DataAccess.Context;
using ESchedule.Domain.Tenant;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.UnitTestsHelpers.Database;

public class TestDbContextBuilder
{
    private Mock<TenantEScheduleDbContext> _dbContext = null!;

    public TestDbContextBuilder CreateTenantDbContext(Guid tenantId)
    {
        var mockDbOptions = new DbContextOptionsBuilder<TenantEScheduleDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var tenantContext = new TenantContext(tenantId);
        var mockTenantContextProvider = new Mock<ITenantContextProvider>();
        mockTenantContextProvider
            .SetupGet(x => x.Current)
            .Returns(tenantContext);

        _dbContext = new Mock<TenantEScheduleDbContext>(mockDbOptions, mockTenantContextProvider.Object);

        return this;
    }

    public TestDbContextBuilder AddDbSet<TEntity>(
        Expression<Func<TenantEScheduleDbContext, DbSet<TEntity>?>> expression,
        IQueryable<TEntity> data = null!,
        Action<Mock<DbSet<TEntity>>> mockConfiguration = null!)
        where TEntity : class
    {
        data ??= new List<TEntity>().AsQueryable();

        var mockDbSet = new Mock<DbSet<TEntity>>();
        mockConfiguration?.Invoke(mockDbSet);

        _dbContext
            .Setup(x => x.Set<TEntity>())
            .ReturnsDbSet(data, mockDbSet);
        _dbContext
            .SetupGet(expression)
            .Returns(mockDbSet.Object);

        return this;
    }

    public Mock<TenantEScheduleDbContext> Build()
    {
        return _dbContext;
    }
}
