namespace BookStore.Contracts.Rest.Books;

public record BookDto(
    int Id,
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
