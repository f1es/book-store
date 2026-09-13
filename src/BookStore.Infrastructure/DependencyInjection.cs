using BookStore.Application.Abstractions.Database.Repositories;
using BookStore.Contracts.Infrastructure.Cache;
using BookStore.Contracts.Infrastructure.Database;
using BookStore.Infrastructure.Cache;
using BookStore.Infrastructure.Database;
using BookStore.Infrastructure.Database.Options;
using BookStore.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .ConfigureDatabase()
            .ConfigureCache();
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<BookStoreDbContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            options.UseSqlServer(databaseOptions.ConnectionString);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(typeof(IBulkRepository<>), typeof(BulkRepository<>));

        return services;
    }

    private static IServiceCollection ConfigureCache(this IServiceCollection services)
    {
        return services;
    }
}
