namespace BookStore.Contracts.Rest.v1.Authors;

public record AuthorDto(
    int Id,
    string FirstName,
    string LastName,
    string Biography,
    DateOnly Birthday,
    string Nationality);
