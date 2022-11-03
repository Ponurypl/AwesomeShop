namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Auth.Register;

public sealed record RegisterRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}