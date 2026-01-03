namespace BookStore.Contracts.Infrastructure.Database.Repositories.Models;

public class PaginationParameters
{
    private int _take;
    private int _page;

    public const int DefaultPageSize = 250;

    public int Take
    {
        get => _take; 
        set => _take = value > 0 
            ? value
            : DefaultPageSize;
    }
    public int Page
    {
        get => _page;
        set => _page = value > 0 
            ? value 
            : 1;
    }

    public PaginationParameters()
    {
        Page = 1;
        Take = DefaultPageSize;
    }

    public PaginationParameters(int page = 1, int pageSize = DefaultPageSize)
    {
        Take = pageSize;
        Page = page;
    }

    public int Skip() => (Page - 1) * Take;
}
