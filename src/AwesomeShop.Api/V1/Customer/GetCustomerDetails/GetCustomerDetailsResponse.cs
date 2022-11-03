namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetCustomerDetails;

public sealed record GetCustomerDetailsResponse
{
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
}