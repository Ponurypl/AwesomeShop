using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart;

public class GetCurrentCartQueryMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderItem, CartItemDto>()
              .Map(d => d.OrderItemId, s => s.Id.Value)
              .Map(d => d.ProductId, s => s.ProductId.Value)
              .Ignore(d => d.ThumbnailFilename!);

        config.NewConfig<Order, CartDto>();
    }
}