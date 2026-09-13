using BookStore.API.Extensions;
using BookStore.API.Mappers;
using BookStore.Application.Abstractions.Database.Models;
using BookStore.Contracts.Applications.Services;
using BookStore.Contracts.Rest.v1.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers.v1;

[ApiController]
[Route("api/v1/publishers")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class PublishersController : ControllerBase
{
    private readonly IPublishersService _publishersService;

    public PublishersController(IPublishersService publishersService)
    {
        _publishersService = publishersService;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PublisherDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPublisher([FromRoute] int id, CancellationToken ct)
    {
        var result = await _publishersService.GetAsync(id, ct);

        return result.ToActionResult(b => b.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedCollection<PublisherDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublishers([FromQuery] PaginationParameters pagination, CancellationToken ct)
    {
        var books = await _publishersService.GetCollectionAsync(pagination, ct);

        return Ok(books.MapTo(b => b.ToResponse()));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PublisherDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePublisher([FromBody] CreatePublisherDto requestDto, CancellationToken ct)
    {
        var result = await _publishersService.CreateAsync(requestDto.ToModel(), ct);

        return result.ToActionResult(b => b.ToResponse());
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePublisher([FromRoute] int id, CancellationToken ct)
    {
        var result = await _publishersService.DeleteAsync(id, ct);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePublisher([FromRoute] int id, [FromBody] CreatePublisherDto requestDto, CancellationToken ct)
    {
        var result = await _publishersService.UpdateAsync(id, requestDto.ToModel(), ct);

        return result.ToActionResult();
    }
}
