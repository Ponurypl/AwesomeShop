using OnboardingIntegrationExample.AwesomeShop.Domain.PrimitiveTypes;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public class User
{
    public UserId Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}