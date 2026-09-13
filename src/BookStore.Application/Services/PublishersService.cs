using BookStore.Application.Abstractions.Database.Models;
using BookStore.Application.Abstractions.Database.Repositories;
using BookStore.Application.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Domain.Models;

namespace BookStore.Application.Services;

public class PublishersService : IPublishersService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBulkRepository<Publisher> _bulkRepository;

    public PublishersService(
        IUnitOfWork unitOfWork,
        IBulkRepository<Publisher> bulkRepository)
    {
        _unitOfWork = unitOfWork;
        _bulkRepository = bulkRepository;
    }

    public async Task<ServiceResult<Publisher>> CreateAsync(Publisher publisher, CancellationToken ct = default)
    {
        _unitOfWork.PublisherRepository.Add(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return publisher;
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var affectedRows = await _bulkRepository.BulkDeleteAsync(p => p.Id == id, ct);

        return affectedRows == 0
            ? ServiceResult.Failure(ResultTypes.NotFound, $"Publisher not found")
            : ServiceResult.Success();
    }

    public async Task<ServiceResult<Publisher>> GetAsync(int id, CancellationToken ct = default)
    {
        var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id, ct: ct);

        return publisher is null
            ? ServiceResult<Publisher>.Failure(ResultTypes.NotFound, $"Publisher not found")
            : publisher;
    }

    public async Task<PagedCollection<Publisher>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        return await _unitOfWork.PublisherRepository.GetPagedCollectionAsync(paginationParameters, ct: ct);
    }

    public async Task<ServiceResult> UpdateAsync(int id, Publisher publisher, CancellationToken ct = default)
    {
        var existingPublisher = await _unitOfWork.PublisherRepository.GetByIdAsync(id, trackChanges: true, ct: ct);
        if (existingPublisher is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Publisher not found");
        }

        existingPublisher.Update(publisher);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
