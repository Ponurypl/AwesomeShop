namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class SavedCardDetails
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
}