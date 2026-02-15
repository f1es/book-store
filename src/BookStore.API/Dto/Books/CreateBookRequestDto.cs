namespace BookStore.API.Dto.Books;

public record CreateBookRequestDto(
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
