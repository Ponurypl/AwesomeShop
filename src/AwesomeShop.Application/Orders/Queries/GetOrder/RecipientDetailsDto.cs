namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed record RecipientDetailsDto
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string AddressLine1 { get; init; } = default!;
    public string? AddressLine2 { get; init; }
    public string City { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
}