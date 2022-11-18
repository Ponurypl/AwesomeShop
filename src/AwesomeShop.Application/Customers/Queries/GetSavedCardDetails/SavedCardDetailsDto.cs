namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetSavedCardDetails;

public sealed record SavedCardDetailsDto
{
    public Guid Id { get; init; }
    public string CardNumber { get; init; } = default!;
    public string ExpiryDate { get; set; } = default!;
}