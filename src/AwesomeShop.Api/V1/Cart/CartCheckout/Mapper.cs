using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.CartCheckout;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PaymentMethods, Application.Orders.Commands.CartCheckout.PaymentMethods>();
        config.NewConfig<CardDetails, Application.Orders.Commands.CartCheckout.CardDetails>();
        config.NewConfig<OrderDto, Order>();
    }
}