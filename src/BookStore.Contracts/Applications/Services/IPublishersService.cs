using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IPublishersService
{
    Task<ServiceResult<Publisher>> CreateAsync(Publisher publisher, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, Publisher publisher, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<Publisher>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<Publisher>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
