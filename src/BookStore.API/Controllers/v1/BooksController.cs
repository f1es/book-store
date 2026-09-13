using BookStore.API.Extensions;
using BookStore.API.Mappers;
using BookStore.Application.Abstractions.Database.Models;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Rest.v1.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1;

[ApiController]
[Route("api/v1/books")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook([FromRoute] int id, CancellationToken ct)
    {
        var result = await _booksService.GetAsync(id, ct);

        return result.ToActionResult(b => b.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedCollection<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooks([FromQuery] PaginationParameters pagination, CancellationToken ct)
    {
        var books = await _booksService.GetCollectionAsync(pagination, ct);

        return Ok(books.MapTo(b => b.ToResponse()));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto requestDto, CancellationToken ct)
    {
        var result = await _booksService.CreateAsync(requestDto.ToModel(), ct);

        return result.ToActionResult(b => b.ToResponse());
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
    public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] CreateBookDto requestDto, CancellationToken ct)
    {
        var result = await _booksService.UpdateAsync(id, requestDto.ToModel(), ct);

        return result.ToActionResult();
    }
}
