namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;

public sealed record TokenDetails
{
    public string TokenId { get; init; } = default!;
    public string CardNumber { get; init; } = default!;
    public string CardHolderName { get; init; } = default!;
    public string ExpiryDate { get; init; } = default!;
}