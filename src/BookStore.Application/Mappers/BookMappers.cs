using BookStore.Domain.Models;

namespace BookStore.Application.Mappers;

public static class BookMappers
{
    public static void Update(this Book existingBook, Book book)
    {
        existingBook.Title = book.Title;
        existingBook.Description = book.Description;
        existingBook.PublicationDate = book.PublicationDate;
        existingBook.Price = book.Price;
        existingBook.AuthorId = book.AuthorId;
        existingBook.PublisherId = book.PublisherId;
    }
}
