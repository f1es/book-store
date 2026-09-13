namespace BookStore.API.Dto.Publishers;

public record PublisherDto(
    int Id,
    string Name,
    string Address,
    string? Website);
