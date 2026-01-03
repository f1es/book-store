using BookStore.Contracts.Applications.Dto.Books;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IBooksService
{
    Task<ServiceResult<BookResponseDto>> CreateAsync(CreateBookRequestDto createBookRequestDto, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, CreateBookRequestDto createBookRequestDto, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<BookResponseDto>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<BookResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
