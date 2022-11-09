using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.GetCurrentCart;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CartDto, CartResponse>();
        config.NewConfig<CartItemDto, CartItem>();
    }
}