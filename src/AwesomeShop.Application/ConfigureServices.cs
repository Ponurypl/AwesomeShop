using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace OnboardingIntegrationExample.AwesomeShop.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
    
}