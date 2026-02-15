namespace BookStore.API.Dto.Publishers;

public record PublisherResponseDto(
    int Id,
    string Name,
    string Address,
    string? Website);
