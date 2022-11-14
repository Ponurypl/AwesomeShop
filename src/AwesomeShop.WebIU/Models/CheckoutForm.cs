namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class CheckoutForm
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string AddressLine1 { get; set; } = default!;
    public string AddressLine2 { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Phone { get; set; } = default!;
}