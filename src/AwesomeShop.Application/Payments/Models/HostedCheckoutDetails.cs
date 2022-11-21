namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;

public sealed record HostedCheckoutDetails
{
    public string Id { get; set; }
    public string RedirectUrl { get; set; }
}