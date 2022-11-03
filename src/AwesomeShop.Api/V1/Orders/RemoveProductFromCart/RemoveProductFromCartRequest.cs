namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.RemoveProductFromCart;

public sealed record RemoveProductFromCartRequest
{
    public Guid OrderItemId { get; set; }
}