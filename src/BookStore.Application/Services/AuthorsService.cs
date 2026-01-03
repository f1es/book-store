using BookStore.Application.Mappers;
using BookStore.Contracts.Applications.Dto.Authors;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Results;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;

namespace BookStore.Application.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<AuthorResponseDto>> CreateAsync(CreateAuthorRequestDto requestDto, CancellationToken ct = default)
    {
        var author = requestDto.ToModel();

        _unitOfWork.AuthorRepository.Add(author);
        await _unitOfWork.SaveChangesAsync(ct);

        return author.ToResponse();
    }

    public async Task<ServiceResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var author = await _unitOfWork
            .AuthorRepository
            .GetByIdAsync(id, ct: ct);

        if (author is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Author with id {id} not found");
        }

        return ServiceResult.Success();
    }

    public async Task<ServiceResult<AuthorResponseDto>> GetAsync(int id, CancellationToken ct = default)
    {
        var author = await _unitOfWork
            .AuthorRepository
            .GetByIdAsync(id, ct: ct);

        if (author is null)
        {
            return ServiceResult<AuthorResponseDto>.Failure(ResultTypes.NotFound, $"Author with id {id} not found");
        }

        return author.ToResponse();
    }

    public async Task<PagedCollection<AuthorResponseDto>> GetCollectionAsync(PaginationParameters paginationParameters, CancellationToken ct = default)
    {
        var authors = await _unitOfWork
            .AuthorRepository
            .GetCollectionAndCountAsync(paginationParameters, ct: ct);

        return authors
            .Data
            .ToResponse()
            .ToPagedCollection(authors.Count, paginationParameters);  
    }

    public async Task<ServiceResult> UpdateAsync(int id, CreateAuthorRequestDto requestDto, CancellationToken ct = default)
    {
        var author = await _unitOfWork
            .AuthorRepository
            .GetByIdAsync(id, trackChanges: true, ct: ct);

        if (author is null)
        {
            return ServiceResult.Failure(ResultTypes.NotFound, $"Author with id {id} not found");
        }

        author.Update(requestDto);
        await _unitOfWork.SaveChangesAsync(ct);

        return ServiceResult.Success();
    }
}
