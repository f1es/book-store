namespace BookStore.Contracts.Rest.Books;

public record CreateBookDto(
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
