namespace BookStore.Infrastructure.Database.Options;

public class DatabaseOptions
{
    public const string Section = "DatabaseOptions";

    public string ConnectionString { get; set; } = default!;
}
