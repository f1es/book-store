using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using BookStore.Domain.Models;

namespace BookStore.Application.Services;

public class BooksService : IBooksService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBulkRepository<Book> _bulkRepository;

    public BooksService(
        IUnitOfWork unitOfWork,
        IBulkRepository<Book> bulkRepository)
    {
        _unitOfWork = unitOfWork;
        _bulkRepository = bulkRepository;
    }

    public async Task<ServiceResult<Book>> GetAsync(int id, CancellationToken ct = default)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id, ct: ct);

        return book is null
            ? ServiceResult<Book>.Failure(ResultTypes.NotFound, $"Book with id {id} not found")
            : book;
    }

    public async Task<PagedCollection<Book>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var books = await _unitOfWork.BookRepository.GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return books.Data.ToPagedCollection(books.Count, paginationParameters);
    }

    public async Task<ServiceResult<Book>> CreateAsync(Book book, CancellationToken ct = default)
    {
        _unitOfWork.BookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return book;
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var affectedRows = await _bulkRepository.BulkDeleteAsync(b => b.Id == id, ct: ct);

        return affectedRows == 0 
            ? ServiceResult.Failure(ResultTypes.NotFound, $"Book with id {id} not found")
            : ServiceResult.Success();
    }

    public async Task<ServiceResult> UpdateAsync(int id, Book book, CancellationToken ct = default)
    {
        var existingBook = await _unitOfWork.BookRepository.GetByIdAsync(id, trackChanges: true, ct: ct);

        if (existingBook is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Book with id {id} not found");
        }

        existingBook.Update(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
