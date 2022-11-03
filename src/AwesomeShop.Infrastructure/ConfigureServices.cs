using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICryptoService, CryptoService>();
        services.AddOptions();
        services.Configure<CosmosDbConnectionDetails>(configuration.GetSection("CosmosDB"));
        //services.ConfigureOptionsWithCryptoService<DatabaseConnectionDetails>(configuration.GetSection("PgDb"));
        //services.AddSingleton<IDbConnectionStringProvider, PostgreDbConnectionStringProvider>();

        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IOrdersRepository, OrdersRepository>();
        services.AddScoped<IPhotosRepository, PhotosRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>();

        return services;
    }

    public static void ApplyDbMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().ApplyMigrations();
    }

    internal static IServiceCollection ConfigureOptionsWithCryptoService<TOptions>(
        this IServiceCollection services, IConfigurationSection configurationSection)
        where TOptions : class, IOptionsWithCryptoService
    {
        services.Configure<TOptions>(configurationSection);
        services.TryAddSingleton<IConfigureOptions<TOptions>, ConfigureOptionsWithCryptoService>();

        return services;
    }
}