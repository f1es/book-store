using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IAuthorsService
{
    Task<ServiceResult<Author>> CreateAsync(Author author, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, Author author, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<Author>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<Author>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
