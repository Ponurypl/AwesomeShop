namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.AddProductToCart;

public sealed record AddProductToCartRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}