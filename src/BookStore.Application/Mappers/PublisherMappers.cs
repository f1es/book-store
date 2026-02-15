using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class PublisherMappers
{
    public static void Update(this Publisher existingPublisher, Publisher publisher)
    {
        existingPublisher.Name = publisher.Name;
        existingPublisher.Address = publisher.Address;
        existingPublisher.Website = publisher.Website;
    }
}
