using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries.GetWebhookEvents;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.GetWebhookEvents;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WebHookEventDto, WebhookEvent>();
    }
}