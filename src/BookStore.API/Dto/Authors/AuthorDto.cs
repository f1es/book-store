namespace BookStore.API.Dto.Authors;

public record AuthorDto(
    int Id,
    string FirstName,
    string LastName,
    string Biography,
    DateOnly Birthday,
    string Nationality);
