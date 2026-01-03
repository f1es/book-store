namespace BookStore.Contracts.Applications.Dto.Publishers;

public record CreatePublisherRequestDto(
    string Name,
    string Address,
    string? Website);
