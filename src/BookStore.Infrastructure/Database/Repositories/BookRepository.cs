using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Domain.Models;

namespace BookStore.Infrastructure.Database.Repositories;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(BookStoreDbContext dbContext) : base(dbContext)
    { }
}
