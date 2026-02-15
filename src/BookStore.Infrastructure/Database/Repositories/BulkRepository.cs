using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Infrastructure.Database.Repositories;

public class BulkRepository<TEntity> : IBulkRepository<TEntity> where TEntity : class, IEntity
{
    private readonly BookStoreDbContext _dbContext;

    public BulkRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        return _dbContext.Set<TEntity>().Where(predicate).ExecuteDeleteAsync(ct);
    }
}
