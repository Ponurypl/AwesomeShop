namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed record CardDetails
{
    public string HolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string ExpirationDate { get; set; } = default!;
    public string CVV { get; set; } = default!;
}