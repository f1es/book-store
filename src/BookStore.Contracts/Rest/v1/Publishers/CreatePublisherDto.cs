namespace BookStore.Contracts.Rest.v1.Publishers;

public record CreatePublisherDto(
    string Name,
    string Address,
    string? Website);
