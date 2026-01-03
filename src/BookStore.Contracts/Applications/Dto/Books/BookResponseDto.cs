namespace BookStore.Contracts.Applications.Dto.Books;

public record BookResponseDto(
    int Id,
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
