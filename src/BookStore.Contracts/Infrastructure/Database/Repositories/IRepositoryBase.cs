using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Abstractions;
using System.Linq.Expressions;

namespace BookStore.Contracts.Infrastructure.Database.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetByIdAsync(
        int id,
        bool trackChanges = false,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<ICollection<TEntity>> GetCollectionAsync(
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<ICollection<TEntity>> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<(ICollection<TEntity> Data, int Count)> GetCollectionAndCountAsync(
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<(ICollection<TEntity> Data, int Count)> GetByPredicateAndCountAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters? paginationParameters = null,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null, 
        CancellationToken ct = default);

    void Add(TEntity entity);

    void AddRange(ICollection<TEntity> entities);

    void Delete(TEntity entity);
}
