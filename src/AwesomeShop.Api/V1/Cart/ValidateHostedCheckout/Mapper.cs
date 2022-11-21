using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.ValidateHostedCheckout;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.ValidateHostedCheckout;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDto, CartCheckout.Order>();
    }
}