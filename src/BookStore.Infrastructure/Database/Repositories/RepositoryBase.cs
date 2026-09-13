using BookStore.Application.Abstractions.Database.Models;
using BookStore.Application.Abstractions.Database.Repositories;
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

        return await query
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public Task<ICollection<TEntity>> GetCollectionAsync(
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        return GetByPredicateAsync(e => true, orderingParameters, includes, ct);
    }

    public async Task<PagedCollection<TEntity>> GetPagedCollectionByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters paginationParameters,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        var count = await _dbContext
            .Set<TEntity>()
            .Where(predicate)
            .CountAsync(ct);

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

        var data = await query
            .Skip(paginationParameters.Skip())
            .Take(paginationParameters.Take)
            .AsNoTracking()
            .ToListAsync(ct);

        return new PagedCollection<TEntity>(data, count, paginationParameters);
    }

    public Task<PagedCollection<TEntity>> GetPagedCollectionAsync(
        PaginationParameters paginationParameters,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default)
    {
        return GetPagedCollectionByPredicateAsync(e => true, paginationParameters, orderingParameters, includes, ct);
    }

    public async Task<bool> NoOneAsync(int id, CancellationToken ct = default)
    {
        return !await _dbContext.Set<TEntity>().AnyAsync(e => e.Id == id, ct);
    }
}
