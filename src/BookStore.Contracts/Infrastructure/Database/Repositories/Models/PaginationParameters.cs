namespace BookStore.Contracts.Infrastructure.Database.Repositories.Models;

public class PaginationParameters
{
    public int Take { get; set; }
    public int Page { get; set; }

    public const int DefaultPageSize = 250;

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
