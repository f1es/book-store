using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Contracts.Applications.Pagination;

public static class ICollectionExtensions
{
    public static PagedCollection<TEntity> ToPagedCollection<TEntity>(
        this ICollection<TEntity> collection,
        int totalCount,
        int page,
        int pageSize)
    {
        return new PagedCollection<TEntity>(collection, totalCount, page, pageSize);
    }

    public static PagedCollection<TEntity> ToPagedCollection<TEntity>(
        this ICollection<TEntity> collection,
        int totalCount,
        PaginationParameters paginationParameters)
    {
        return collection.ToPagedCollection(totalCount, paginationParameters.Page, paginationParameters.Take);
    }
}
