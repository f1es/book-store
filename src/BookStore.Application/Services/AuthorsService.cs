using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Models;

namespace BookStore.Application.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBulkRepository<Author> _bulkRepository;

    public AuthorsService(
        IUnitOfWork unitOfWork, 
        IBulkRepository<Author> bulkRepository)
    {
        _unitOfWork = unitOfWork;
        _bulkRepository = bulkRepository;
    }

    public async Task<ServiceResult<Author>> CreateAsync(Author author, CancellationToken ct = default)
    {
        _unitOfWork.AuthorRepository.Add(author);
        await _unitOfWork.SaveChangesAsync(ct);

        return author;
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var affectedRows = await _bulkRepository.BulkDeleteAsync(a => a.Id == id, ct: ct);

        return affectedRows == 0
            ? ServiceResult.Failure(ResultTypes.NotFound, $"Author with id {id} not found")
            : ServiceResult.Success();
    }

    public async Task<ServiceResult<Author>> GetAsync(int id, CancellationToken ct = default)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id, ct: ct);

        return author is null
            ? ServiceResult<Author>.Failure(ResultTypes.NotFound, $"Author with id {id} not found")
            : author;
    }

    public async Task<PagedCollection<Author>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var authors = await _unitOfWork.AuthorRepository.GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return authors.Data.ToPagedCollection(authors.Count, paginationParameters);  
    }

    public async Task<ServiceResult> UpdateAsync(int id, Author author, CancellationToken ct = default)
    {
        var existingAuthor = await _unitOfWork.AuthorRepository.GetByIdAsync(id, trackChanges: true, ct: ct);

        if (existingAuthor is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Author with id {id} not found");
        }

        existingAuthor.Update(author);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
