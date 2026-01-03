using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Infrastructure.Database.Repositories;

namespace BookStore.Infrastructure.Database;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookStoreDbContext _dbContext;

    public IBookRepository BookRepository => new Lazy<IBookRepository>(() => new BookRepository(_dbContext)).Value;

    public IAuthorRepository AuthorRepository => new Lazy<IAuthorRepository>(() => new AuthorRepository(_dbContext)).Value;

    public IPublisherRepository PublisherRepository => new Lazy<IPublisherRepository>(() => new PublisherRepository(_dbContext)).Value;

    public UnitOfWork(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}
