namespace BookStore.API.Dto.Authors;

public record CreateAuthorRequestDto(
    string FirstName,
    string LastName,
    string Biography,
    DateOnly Birthday,
    string Nationality);
