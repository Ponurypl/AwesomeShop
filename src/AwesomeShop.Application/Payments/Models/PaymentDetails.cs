namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;

public sealed record PaymentDetails
{
    public string PaymentId { get; init; } = null!;
    public int StatusCode { get; init; }
}