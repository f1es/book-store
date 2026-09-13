namespace BookStore.Contracts.Rest.v1.Books;

public record BookDto(
    int Id,
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
