using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, GetOrders.OrderDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString());

        config.NewConfig<OrderItem, OrderItemDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.ProductId, s => s.ProductId.Value);
    }
}