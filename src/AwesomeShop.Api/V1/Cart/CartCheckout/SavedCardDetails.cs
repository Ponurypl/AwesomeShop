namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.CartCheckout;

public sealed record SavedCardDetails
{
    public Guid Id { get; set; }
    public string CVV { get; set; }
}