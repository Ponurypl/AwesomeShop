namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.VerifyCustomer;

public sealed record VerifyCustomerDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
}