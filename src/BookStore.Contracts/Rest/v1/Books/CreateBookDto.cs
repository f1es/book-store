namespace BookStore.Contracts.Rest.v1.Books;

public record CreateBookDto(
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
