using BookStore.Application.Abstractions.Database.Models;
using BookStore.Domain.Abstractions;
using System.Linq.Expressions;

namespace BookStore.Application.Abstractions.Database.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetByIdAsync(
        int id,
        bool trackChanges = false,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<ICollection<TEntity>> GetCollectionAsync(
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<ICollection<TEntity>> GetByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<PagedCollection<TEntity>> GetPagedCollectionAsync(
        PaginationParameters paginationParameters,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    Task<PagedCollection<TEntity>> GetPagedCollectionByPredicateAsync(
        Expression<Func<TEntity, bool>> predicate,
        PaginationParameters paginationParameters,
        OrderingParameters<TEntity>? orderingParameters = null,
        Expression<Func<TEntity, object>>[]? includes = null,
        CancellationToken ct = default);

    void Add(TEntity entity);

    void AddRange(ICollection<TEntity> entities);

    void Delete(TEntity entity);

    Task<bool> NoOneAsync(int id, CancellationToken ct = default);
}
