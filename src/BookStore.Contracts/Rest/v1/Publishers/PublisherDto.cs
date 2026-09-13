namespace BookStore.Contracts.Rest.v1.Publishers;

public record PublisherDto(
    int Id,
    string Name,
    string Address,
    string? Website);
