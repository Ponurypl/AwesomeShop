using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<WebHookEvent, WebHookEventDto>().Map(d => d.Id, s => s.Id.Value);
    }
}