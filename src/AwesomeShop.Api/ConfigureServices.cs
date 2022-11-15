using MapsterMapper;
using OnlinePayments.Sdk.Webhooks;

namespace OnboardingIntegrationExample.AwesomeShop.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApiLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        InMemorySecretKeyStore.Instance.StoreSecretKey(configuration.GetSection("Worldline:Webhook:Key").Value,
                                                       configuration.GetSection("Worldline:Webhook:Secret").Value);

        return services;
    }
}