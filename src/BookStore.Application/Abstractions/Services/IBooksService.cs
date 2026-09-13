using BookStore.Application.Abstractions.Database.Models;
using BookStore.Application.Results;
using BookStore.Domain.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IBooksService
{
    Task<ServiceResult<Book>> CreateAsync(Book book, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, Book book, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<Book>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<Book>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
