namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.UpdateProductsInCart;

public sealed record CartItem
{
    public Guid OrderItemId { get; set; }
    public int Quantity { get; set; }
}