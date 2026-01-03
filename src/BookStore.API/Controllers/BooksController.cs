using BookStore.API.Extensions;
using BookStore.Contracts.Applications.Dto.Books;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/books")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook([FromRoute] int id, CancellationToken ct)
    {
        var result = await _booksService.GetAsync(id, ct);

        return result.ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedCollection<BookResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks([FromQuery] PaginationParameters pagination, CancellationToken ct)
    {
        var books = await _booksService.GetCollectionAsync(pagination, ct);

        return Ok(books);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequestDto requestDto, CancellationToken ct)
    {
        var result = await _booksService.CreateAsync(requestDto, ct);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, CancellationToken ct)
    {
        var result = await _booksService.DeleteAsync(id, ct);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] CreateBookRequestDto requestDto, CancellationToken ct)
    {
        var result = await _booksService.UpdateAsync(id, requestDto, ct);

        return result.ToActionResult();
    }
}
