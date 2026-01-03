using BookStore.Domain.Abstractions;

namespace BookStore.Domain.Models;

public sealed class Author : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Biography { get; set; } = default!;
    public DateOnly Birthday { get; set; } = default!;
    public string Nationality { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
