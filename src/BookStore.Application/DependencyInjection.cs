using BookStore.Application.Services;
using BookStore.Contracts.Applications.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .ConfigureServices();
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBooksService, BooksService>();
        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddScoped<IPublishersService, PublishersService>();

        return services;
    }
}
