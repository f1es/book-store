namespace BookStore.Infrastructure.Cache.Options;

public class RedisOptions
{
    public const string Section = "RedisOptions";

    public string Server { get; set; } = default!;
}
