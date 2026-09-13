namespace BookStore.Contracts.Rest.Authors;

public record AuthorDto(
    int Id,
    string FirstName,
    string LastName,
    string Biography,
    DateOnly Birthday,
    string Nationality);
