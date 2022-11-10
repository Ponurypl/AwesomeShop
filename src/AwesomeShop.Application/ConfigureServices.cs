using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Services;

namespace OnboardingIntegrationExample.AwesomeShop.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.TryAddScoped<IOrderNumberGenService, OrderNumberGenService>();

        return services;
    }
    
}