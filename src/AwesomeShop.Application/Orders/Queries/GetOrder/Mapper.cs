using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.ValueTypes;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Status, s => s.Status.ToString());

        config.NewConfig<OrderItem, OrderItemDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.ProductId, s => s.ProductId.Value);

        config.NewConfig<RecipientDetails, RecipientDetailsDto>();
    }
}