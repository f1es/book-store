using System.Linq.Expressions;

namespace BookStore.Contracts.Infrastructure.Cache;

public interface ICacheService
{
    Task<TEntity> GetOrSetAsync<TEntity>(
        string key,
        Func<Task<TEntity>> factory,
        TimeSpan? expiry = null);

    Task<ISet<TEntity>> GetOrSetHashSetAsync<TEntity>(
        string key,
        Func<Task<ICollection<TEntity>>> factory,
        Expression<Func<TEntity, object>> hashSetKey,
        TimeSpan? expiry = null);

    Task<bool> InvalidateKeyAsync(string key);

    Task<bool> InvalidateHashFieldAsync(string key, string fieldKey);
}
