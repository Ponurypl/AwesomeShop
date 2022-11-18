namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class CreditCard
{
    public string HolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string ExpirationDate { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public bool SaveCardDetails { get; set; }
}