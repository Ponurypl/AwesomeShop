namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
}