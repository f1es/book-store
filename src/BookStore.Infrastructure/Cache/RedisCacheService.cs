using BookStore.Contracts.Infrastructure.Cache;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace BookStore.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(
        IConnectionMultiplexer connectionMultiplexer,
        ILogger<RedisCacheService> logger)
    {
        _database = connectionMultiplexer.GetDatabase();
        _logger = logger;
    }

    public async Task<TEntity> GetOrSetAsync<TEntity>(
        string key,
        Func<Task<TEntity>> factory,
        TimeSpan? expiry = null)
    {
        try
        {
            var redisValue = await _database.StringGetAsync(key);
            if (redisValue.HasValue) 
            { 
                return JsonSerializer.Deserialize<TEntity>(redisValue!)!; 
            }

            var value = await factory();
            if (value is not null) 
            { 
                await SetAsJsonAsync(value, key, expiry); 
            }

            return value;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "[RedisCacheService] Failed to process GetOrSetAsync");

            return await factory();
        }
    }

    public Task<ISet<TEntity>> GetOrSetHashSetAsync<TEntity>(
        string key,
        Func<Task<ICollection<TEntity>>> factory,
        Expression<Func<TEntity, object>> hashSetKey,
        TimeSpan? expiry = null)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InvalidateHashFieldAsync(string key, string fieldKey)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> InvalidateKeyAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    private async Task<bool> SetAsJsonAsync(
        object obj,
        string key,
        TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(obj);
        var value = new RedisValue(serializedValue);
        var result = await _database.StringSetAsync(key, value, expiry: expiry, when: When.Always);

        return result;
    }
}
