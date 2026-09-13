using BookStore.API.Dto.Publishers;
using BookStore.Domain.Models;

namespace BookStore.API.Mappers;

public static class PublisherMappers
{
    public static Publisher ToModel(this CreatePublisherDto requestDto) =>
        new Publisher()
        {
            Name = requestDto.Name,
            Address = requestDto.Address,
            Website = requestDto.Website,
        };

    public static PublisherDto ToResponse(this Publisher publisher) =>
        new PublisherDto(
            publisher.Id,
            publisher.Name,
            publisher.Address,
            publisher.Website);
}
