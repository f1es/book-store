using BookStore.Domain.Abstractions;

namespace BookStore.Domain.Models;

public sealed class Book : IEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public int PublisherId { get; set; }

    public Author Author { get; set; } = default!;
    public Publisher Publisher { get; set; } = default!;

    public void Update(Book book)
    {
        Title = book.Title;
        Description = book.Description;
        PublicationDate = book.PublicationDate;
        Price = book.Price;
        AuthorId = book.AuthorId;
        PublisherId = book.PublisherId;
    }
}
