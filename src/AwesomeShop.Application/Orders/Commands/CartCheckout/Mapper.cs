using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>().Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString());
    }
}