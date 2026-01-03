using BookStore.Contracts.Applications.Dto.Books;
using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class BookMappers
{
    public static Book ToModel(this CreateBookRequestDto bookRequestDto) =>
        new Book
        {
            Title = bookRequestDto.Title,
            Description = bookRequestDto.Description,
            PublicationDate = bookRequestDto.PublicationDate,
            Price = bookRequestDto.Price,
            AuthorId = bookRequestDto.AuthorId,
            PublisherId = bookRequestDto.PublisherId,
        };

    public static BookResponseDto ToResponse(this Book book) =>
        new BookResponseDto(
            book.Id,
            book.Title,
            book.Description,
            book.PublicationDate,
            book.Price,
            book.AuthorId,
            book.PublisherId);

    public static ICollection<BookResponseDto> ToResponse(this ICollection<Book> books) => 
        books.Select(ToResponse).ToList();

    public static void Update(this Book book, CreateBookRequestDto requestDto)
    {
        book.Title = requestDto.Title;
        book.Description = requestDto.Description;
        book.PublicationDate = requestDto.PublicationDate;
        book.Price = requestDto.Price;
        book.AuthorId = requestDto.AuthorId;
        book.PublisherId = requestDto.PublisherId;
    }
}
