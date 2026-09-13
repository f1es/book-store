namespace BookStore.Contracts.Infrastructure.Cache;

public interface ICacheService
{
    Task<TEntity> GetOrSetAsync<TEntity>(
        string key,
        Func<Task<TEntity>> factory,
        TimeSpan? expiry = null);

    Task<bool> InvalidateKeyAsync(string key);
}
