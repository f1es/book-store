using BookStore.Application.Abstractions.Database.Models;
using BookStore.Application.Abstractions.Database.Repositories;
using BookStore.Application.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
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
            ? ServiceResult<Book>.NotFound("Book not found")
            : ServiceResult<Book>.Success(book);
    }

    public async Task<PagedCollection<Book>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        return await _unitOfWork.BookRepository.GetPagedCollectionAsync(paginationParameters, ct: ct);
    }

    public async Task<ServiceResult<Book>> CreateAsync(Book book, CancellationToken ct = default)
    {
        var hasNoAuthor = await _unitOfWork.AuthorRepository.NoOneAsync(book.AuthorId, ct: ct);
        if (hasNoAuthor)
        {
            return ServiceResult<Book>.NotFound("Author not found");
        }

        var hasNoPublisher = await _unitOfWork.PublisherRepository.NoOneAsync(book.PublisherId, ct: ct);
        if (hasNoPublisher)
        {
            return ServiceResult<Book>.NotFound("Publisher not found");
        }

        _unitOfWork.BookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return book;
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var affectedRows = await _bulkRepository.BulkDeleteAsync(b => b.Id == id, ct: ct);

        return affectedRows == 0
            ? ServiceResult.NotFound("Book not found")
            : ServiceResult.Success();
    }

    public async Task<ServiceResult> UpdateAsync(int id, Book book, CancellationToken ct = default)
    {
        var hasNoAuthor = await _unitOfWork.AuthorRepository.NoOneAsync(book.AuthorId, ct: ct);
        if (hasNoAuthor)
        {
            return ServiceResult.NotFound("Author not found");
        }

        var hasNoPublisher = await _unitOfWork.PublisherRepository.NoOneAsync(book.PublisherId, ct: ct);
        if (hasNoPublisher)
        {
            return ServiceResult.NotFound("Publisher not found");
        }

        var existingBook = await _unitOfWork.BookRepository.GetByIdAsync(id, trackChanges: true, ct: ct);
        if (existingBook is null)
        {
            return ServiceResult.NotFound("Book not found");
        }

        existingBook.Update(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
