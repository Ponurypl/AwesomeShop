using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICryptoService, CryptoService>();
        services.ConfigureOptionsWithCryptoService<DatabaseConnectionDetails>(configuration.GetSection("PgDb"));
        services.AddSingleton<IDbConnectionStringProvider, PostgreDbConnectionStringProvider>();

        services.AddDbContext<ApplicationDbContext>();

        return services;
    }

    internal static IServiceCollection ConfigureOptionsWithCryptoService<TOptions>(
        this IServiceCollection services, IConfigurationSection configurationSection)
        where TOptions : class, IOptionsWithCryptoService
    {
        services.TryAddTransient<IConfigureOptions<IOptionsWithCryptoService>, ConfigureOptionsWithCryptoService>();
        services.Configure<TOptions>(configurationSection);
        services.ConfigureOptions<TOptions>();

        return services;
    }
}