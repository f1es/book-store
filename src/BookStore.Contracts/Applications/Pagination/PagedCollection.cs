using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using System.Collections.Immutable;

namespace BookStore.Contracts.Applications.Pagination;

public class PagedCollection<TEntity>
{
    public IReadOnlyCollection<TEntity> Data { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int Page { get; }
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;
    public int Count => Data.Count;

    public PagedCollection(
        ICollection<TEntity> data,
        int entitiesTotalCount,
        int page,
        int pageSize)
    {
        Data = data.ToImmutableList();
        TotalPages = entitiesTotalCount;
        Page = page;
        PageSize = pageSize;
    }

    public PagedCollection(
        ICollection<TEntity> data,
        int entitiesTotalCount,
        PaginationParameters paginationParameters)
    {
        Data = data.ToImmutableList();
        TotalPages = entitiesTotalCount;
        Page = paginationParameters.Page;
        PageSize = paginationParameters.Take;
    }
}
