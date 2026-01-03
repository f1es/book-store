using BookStore.Contracts.Applications.Dto.Publishers;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Contracts.Applications.Services;

public interface IPublishersService
{
    Task<ServiceResult<PublisherResponseDto>> CreateAsync(CreatePublisherRequestDto createPublisherRequestDto, CancellationToken ct = default);
    Task<ServiceResult> UpdateAsync(int id, CreatePublisherRequestDto createPublisherRequestDto, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<ServiceResult<PublisherResponseDto>> GetAsync(int id, CancellationToken ct = default);
    Task<PagedCollection<PublisherResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default);
}
