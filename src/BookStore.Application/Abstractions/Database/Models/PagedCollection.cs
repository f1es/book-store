using System.Collections.Immutable;

namespace BookStore.Application.Abstractions.Database.Models;

public class PagedCollection<TEntity>
{
    public int TotalPages { get; }
    public int Page { get; }
    public int TotalCount { get; }
    public int PageSize { get; }
    public bool HasNext => Page < TotalPages;
    public bool HasPrevious => Page > 1;
    public IReadOnlyCollection<TEntity> Data { get; }

    public PagedCollection(
        ICollection<TEntity> data,
        int totalCount,
        int page,
        int pageSize)
    {
        Data = data.ToImmutableList();
        TotalCount = totalCount;
        TotalPages = CalculateTotalPages(totalCount, pageSize);
        Page = page;
        PageSize = pageSize;
    }

    public PagedCollection(
        ICollection<TEntity> data,
        int totalCount,
        PaginationParameters paginationParameters)
    {
        Data = data.ToImmutableList();
        TotalPages = CalculateTotalPages(totalCount, paginationParameters.Take);
        TotalCount = totalCount;
        Page = paginationParameters.Page;
        PageSize = paginationParameters.Take;
    }

    public PagedCollection<TOut> MapTo<TOut>(Func<TEntity, TOut> mapper)
    {
        return new PagedCollection<TOut>(
            Data.Select(e => mapper(e)).ToList(),
            TotalCount,
            Page,
            PageSize);
    }

    private int CalculateTotalPages(int totalCount, int pageSize)
    {
        return (int)Math.Ceiling((double)totalCount / (double)pageSize);
    }
}
