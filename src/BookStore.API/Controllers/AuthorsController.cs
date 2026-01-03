using BookStore.API.Extensions;
using BookStore.Contracts.Applications.Dto.Authors;
using BookStore.Contracts.Applications.Pagination;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Infrastructure.Database.Repositories.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/authors")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor([FromRoute] int id, CancellationToken ct)
    {
        var result = await _authorsService.GetAsync(id, ct);

        return result.ToActionResult();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedCollection<AuthorResponseDto>))]
    public async Task<IActionResult> GetAuthors([FromQuery] PaginationParameters pagination, CancellationToken ct)
    {
        var authors = await _authorsService.GetCollectionAsync(pagination, ct);

        return Ok(authors);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequestDto requestDto, CancellationToken ct)
    {
        var result = await _authorsService.CreateAsync(requestDto, ct);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuthor([FromRoute] int id, CancellationToken ct)
    {
        var result = await _authorsService.DeleteAsync(id, ct);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] CreateAuthorRequestDto requestDto, CancellationToken ct)
    {
        var result = await _authorsService.UpdateAsync(id, requestDto, ct);

        return result.ToActionResult();
    }
}
