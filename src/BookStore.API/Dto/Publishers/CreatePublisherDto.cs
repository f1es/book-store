namespace BookStore.API.Dto.Publishers;

public record CreatePublisherDto(
    string Name,
    string Address,
    string? Website);
