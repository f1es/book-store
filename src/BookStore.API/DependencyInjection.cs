using BookStore.API.Validators;
using BookStore.Infrastructure.Database.Options;
using FluentValidation;
using System.Reflection;

namespace BookStore.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services
            .ConfigureOptions(configuration)
            .ConfigureSwagger()
            .ConfigureValidators();
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Section));

        return services;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    private static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssemblies(Assembly.GetAssembly(typeof(string)));

        return services;
    }
}
