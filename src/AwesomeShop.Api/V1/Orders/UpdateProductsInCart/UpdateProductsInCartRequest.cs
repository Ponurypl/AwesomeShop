namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.UpdateProductsInCart;

public record UpdateProductsInCartRequest
{
    public List<CartItem> ItemsToUpdate { get; set; } = new();
}