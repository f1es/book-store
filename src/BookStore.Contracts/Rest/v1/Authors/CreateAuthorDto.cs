namespace BookStore.Contracts.Rest.v1.Authors;

public record CreateAuthorDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Biography { get; set; } = default!;
    public DateOnly Birthday { get; set; } = default!;
    public string Nationality { get; set; } = default!;
}
