using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Domain.Models;

namespace BookStore.Infrastructure.Database.Repositories;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(BookStoreDbContext dbContext) : base(dbContext)
    { }
}
