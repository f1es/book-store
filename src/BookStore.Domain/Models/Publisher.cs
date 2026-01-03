using BookStore.Domain.Abstractions;

namespace BookStore.Domain.Models;

public sealed class Publisher : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Website { get; set; } = default!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
