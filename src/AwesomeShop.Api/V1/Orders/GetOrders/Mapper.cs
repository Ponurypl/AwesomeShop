using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrders;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrders;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDto, Order>();
    }
}