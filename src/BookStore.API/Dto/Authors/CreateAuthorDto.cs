namespace BookStore.API.Dto.Authors;

public record CreateAuthorDto(
    string FirstName,
    string LastName,
    string Biography,
    DateOnly Birthday,
    string Nationality);
