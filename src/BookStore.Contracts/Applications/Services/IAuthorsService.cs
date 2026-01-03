using BookStore.Contracts.Applications.Dto.Authors;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IAuthorsService
{
    Task<ServiceResult<AuthorResponseDto>> CreateAsync(CreateAuthorRequestDto requestDto, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, CreateAuthorRequestDto requestDto, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<AuthorResponseDto>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<AuthorResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
