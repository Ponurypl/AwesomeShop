using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDto, Order>();
        config.NewConfig<OrderItemDto, OrderItem>();
        config.NewConfig<RecipientDetailsDto, RecipientDetails>();
    }
}