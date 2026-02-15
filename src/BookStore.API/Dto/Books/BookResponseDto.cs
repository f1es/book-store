namespace BookStore.API.Dto.Books;

public record BookResponseDto(
    int Id,
    string Title,
    string? Description,
    DateOnly PublicationDate,
    decimal Price,
    int AuthorId,
    int PublisherId);
