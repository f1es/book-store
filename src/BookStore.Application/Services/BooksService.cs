using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Dto.Books;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Application.Services;

public class BooksService : IBooksService
{
    private readonly IUnitOfWork _unitOfWork;

    public BooksService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<BookResponseDto>> GetAsync(int id, CancellationToken ct = default)
    {
        var book = await _unitOfWork
            .BookRepository
            .GetByIdAsync(id, ct: ct);

        if (book is null)
        {
            return ServiceResult<BookResponseDto>.Failure(ResultTypes.NotFound, $"Book with id {id} not found");
        }

        return book.ToResponse();
    }

    public async Task<PagedCollection<BookResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var books = await _unitOfWork
            .BookRepository
            .GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return books
            .Data
            .ToResponse()
            .ToPagedCollection(books.Count, paginationParameters);
    }

    public async Task<ServiceResult<BookResponseDto>> CreateAsync(CreateBookRequestDto createBookRequestDto, CancellationToken ct = default)
    {
        var book = createBookRequestDto.ToModel();

        _unitOfWork.BookRepository.Add(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return book.ToResponse();
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var book = await _unitOfWork
            .BookRepository
            .GetByIdAsync(id, ct: ct);

        if (book is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Book with id {id} not found");
        }

        _unitOfWork.BookRepository.Delete(book);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> UpdateAsync(int id, CreateBookRequestDto createBookRequestDto, CancellationToken ct = default)
    {
        var book = await _unitOfWork
            .BookRepository
            .GetByIdAsync(id, trackChanges: true, ct: ct);

        if (book is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Book with id {id} not found");
        }

        book.Update(createBookRequestDto);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
