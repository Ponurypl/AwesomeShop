namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;

public sealed record RecipientDetails
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string AddressLine1 { get; init; } = default!;
    public string? AddressLine2 { get; init; }
    public string City { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
}