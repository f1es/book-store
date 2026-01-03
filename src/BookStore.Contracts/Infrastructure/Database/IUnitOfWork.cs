using BookStore.Contracts.Infrastructure.Database.Repositories;

namespace BookStore.Contracts.Infrastructure.Database;

public interface IUnitOfWork
{
    IBookRepository BookRepository { get; }

    IAuthorRepository AuthorRepository { get; }

    IPublisherRepository PublisherRepository { get; }

    Task SaveChangesAsync(CancellationToken ct = default);
}
