namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.UpdateProductsInCart;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CartItem, Application.Orders.Commands.UpdateProductsInCart.CartItem>()
              .MapToConstructor(true);
    }
}