namespace BookStore.API.Dto.Books;

public record CreateBookDto(
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
