namespace BookStore.Contracts.Rest.Publishers;

public record PublisherDto(
    int Id,
    string Name,
    string Address,
    string? Website);
