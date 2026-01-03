using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Infrastructure.Database.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
{
    private readonly BookStoreDbContext _dbContext;

    public RepositoryBase(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken ct = default)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable<TEntity>();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync(ct);
    }

    public async Task<TEntity?> GetByIdAsync(
        int id, 
        bool trackChanges = false, 
        Expression<Func<TEntity, object>>[]? includes = null, 
        CancellationToken ct = default)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => 
                current.Include(include));
        }

        return await query.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<ICollection<TEntity>> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) =>
                current.Include(include));
        }

        query = query.Where(predicate);

        if (orderingParameters is not null)
        {
            query = orderingParameters.Order == Order.Ascending
                ? query.OrderBy(orderingParameters.OrderingProperty)
                : query.OrderByDescending(orderingParameters.OrderingProperty);
        }
        else
        {
            query = query
                .OrderBy(e => e.Id);
        }

        if (paginationParameters is not null)
        {
            query = query
                .Skip(paginationParameters.Skip())
                .Take(paginationParameters.Take);
        }

        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<ICollection<TEntity>> GetCollectionAsync(
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        return await GetByPredicateAsync(e => true, paginationParameters, orderingParameters, includes, ct);
    }

    public async Task<(ICollection<TEntity> Data, int Count)> GetByPredicateAndCountAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        var count = await _dbContext
            .Set<TEntity>()
            .Where(predicate)
            .CountAsync(ct);

        var data = await GetByPredicateAsync(
            predicate, 
            paginationParameters, 
            orderingParameters, 
            includes,
            ct);

        return (data, count);
    }

    public async Task<(ICollection<TEntity> Data, int Count)> GetCollectionAndCountAsync(
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        return await GetByPredicateAndCountAsync(e => true, paginationParameters, orderingParameters, includes, ct);
    }
}
