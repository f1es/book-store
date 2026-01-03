using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Dto.Publishers;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Application.Services;

public class PublishersService : IPublishersService
{
    private readonly IUnitOfWork _unitOfWork;

    public PublishersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<PublisherResponseDto>> CreateAsync(CreatePublisherRequestDto createPublisherRequestDto, CancellationToken ct = default)
    {
        var publisher = createPublisherRequestDto.ToModel();

        _unitOfWork.PublisherRepository.Add(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return publisher.ToResponse();
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork
            .PublisherRepository
            .GetByIdAsync(id, ct: ct);

        if (publisher is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        _unitOfWork.PublisherRepository.Delete(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<PublisherResponseDto>> GetAsync(int id, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork
            .PublisherRepository
            .GetByIdAsync(id, ct: ct);

        if (publisher is null)
        {
            return ServiceResult<PublisherResponseDto>.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        return publisher.ToResponse();
    }

    public async Task<PagedCollection<PublisherResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var publishers = await _unitOfWork
            .PublisherRepository
            .GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return publishers
            .Data
            .ToResponse()
            .ToPagedCollection(publishers.Count, paginationParameters);
    }

    public async Task<ServiceResult> UpdateAsync(int id, CreatePublisherRequestDto createPublisherRequestDto, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork
            .PublisherRepository
            .GetByIdAsync(id, trackChanges: true, ct: ct);

        if (publisher is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        publisher.Update(createPublisherRequestDto);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
