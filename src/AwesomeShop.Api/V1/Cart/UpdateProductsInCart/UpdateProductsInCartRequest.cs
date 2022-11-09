namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.UpdateProductsInCart;

public record UpdateProductsInCartRequest
{
    public List<CartItem> ItemsToUpdate { get; set; } = new();
}