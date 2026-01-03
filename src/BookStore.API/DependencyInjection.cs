using BookStore.Infrastructure.Database.Options;

namespace BookStore.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services
            .ConfigureOptions(configuration)
            .ConfigureSwagger();
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseOptions"));
        return services;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }
}
