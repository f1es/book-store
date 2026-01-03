using BookStore.Contracts.Applications.Dto.Publishers;
using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class PublisherMappers
{
    public static Publisher ToModel(this CreatePublisherRequestDto requestDto) =>
        new Publisher()
        {
            Name = requestDto.Name,
            Address = requestDto.Address,
            Website = requestDto.Website,
        };

    public static PublisherResponseDto ToResponse(this Publisher publisher) =>
        new PublisherResponseDto(
            publisher.Id,
            publisher.Name,
            publisher.Address,
            publisher.Website);

    public static ICollection<PublisherResponseDto> ToResponse(this ICollection<Publisher> publishers) => 
        publishers.Select(ToResponse).ToList();

    public static void Update(this Publisher publisher, CreatePublisherRequestDto requestDto)
    {
        publisher.Name = requestDto.Name;
        publisher.Address = requestDto.Address;
        publisher.Website = requestDto.Website;
    }
}
