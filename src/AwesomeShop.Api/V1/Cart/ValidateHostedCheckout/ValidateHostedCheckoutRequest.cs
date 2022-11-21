namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.ValidateHostedCheckout;

public sealed record ValidateHostedCheckoutRequest
{
    public Guid CheckoutId { get; set; }
    public string HostedPaymentId { get; set; }
}