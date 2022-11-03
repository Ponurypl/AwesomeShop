using MapsterMapper;

namespace OnboardingIntegrationExample.AwesomeShop.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiLayerServices(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}