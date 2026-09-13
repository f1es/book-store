namespace BookStore.Contracts.Rest.Publishers;

public record CreatePublisherDto(
    string Name,
    string Address,
    string? Website);
