namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Cart.CartCheckout;

public sealed record CartCheckoutRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string AddressLine1 { get; set; } = default!;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public PaymentMethods PaymentMethod { get; set; }
    public CardDetails? CardDetails { get; set; }
    public SavedCardDetails? SavedCard { get; set; }
}