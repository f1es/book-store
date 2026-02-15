using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Models;

namespace BookStore.Application.Services;

public class PublishersService : IPublishersService
{
    private readonly IUnitOfWork _unitOfWork;

    public PublishersService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<Publisher>> CreateAsync(Publisher publisher, CancellationToken ct = default)
    {
        _unitOfWork.PublisherRepository.Add(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return publisher;
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id, ct: ct);

        if (publisher is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        _unitOfWork.PublisherRepository.Delete(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<Publisher>> GetAsync(int id, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id, ct: ct);

        if (publisher is null)
        {
            return ServiceResult<Publisher>.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        return publisher;
    }

    public async Task<PagedCollection<Publisher>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var publishers = await _unitOfWork.PublisherRepository.GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return publishers.Data.ToPagedCollection(publishers.Count, paginationParameters);
    }

    public async Task<ServiceResult> UpdateAsync(int id, Publisher publisher, CancellationToken ct = default)
    {
        var existingPublisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id, trackChanges: true, ct: ct);

        if (existingPublisher is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Publisher with id {id} not found");
        }

        existingPublisher.Update(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
