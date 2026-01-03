using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Domain.Models;

namespace BookStore.Infrastructure.Database.Repositories;

public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
{
    public PublisherRepository(BookStoreDbContext dbContext) : base(dbContext)
    { }
}
