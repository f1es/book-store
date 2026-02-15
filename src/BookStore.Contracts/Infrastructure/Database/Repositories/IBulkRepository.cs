using BookStore.Domain.Abstractions;
using System.Linq.Expressions;

namespace BookStore.Contracts.Infrastructure.Database.Repositories;

public interface IBulkRepository<TEntity> where TEntity : class, IEntity
{
    Task<int> BulkDeleteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);
}
