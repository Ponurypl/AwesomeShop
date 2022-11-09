namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.RemoveProductFromCart;

public sealed record RemoveProductFromCartRequest
{
    public Guid OrderItemId { get; set; }
}