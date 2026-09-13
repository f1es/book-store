using BookStore.Contracts.Rest.v1.Books;
using BookStore.Domain.Models;

namespace BookStore.API.Mappers;

public static class BookMappers
{
    public static Book ToModel(this CreateBookDto bookRequestDto) =>
        new Book
        {
            Title = bookRequestDto.Title,
            Description = bookRequestDto.Description,
            PublicationDate = bookRequestDto.PublicationDate,
            Price = bookRequestDto.Price,
            AuthorId = bookRequestDto.AuthorId,
            PublisherId = bookRequestDto.PublisherId,
        };

    public static BookDto ToResponse(this Book book) =>
        new BookDto(
            book.Id,
            book.Title,
            book.Description,
            book.PublicationDate,
            book.Price,
            book.AuthorId,
            book.PublisherId);
}
