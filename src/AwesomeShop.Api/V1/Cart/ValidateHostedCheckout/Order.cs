namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.ValidateHostedCheckout;

public sealed record Order
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
}