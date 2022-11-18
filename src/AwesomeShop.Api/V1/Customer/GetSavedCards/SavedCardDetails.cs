namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetSavedCards;

public sealed record SavedCardDetails
{
    public Guid Id { get; init; }
    public string CardNumber { get; init; } = default!;
    public string ExpiryDate { get; set; } = default!;
}