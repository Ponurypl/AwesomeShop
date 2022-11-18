namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed record SavedCardDetails
{
    public Guid Id { get; set; }
    public string CVV { get; set; }
}