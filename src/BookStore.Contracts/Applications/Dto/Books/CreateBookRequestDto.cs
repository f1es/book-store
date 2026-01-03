namespace BookStore.Contracts.Applications.Dto.Books;

public record CreateBookRequestDto(
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
