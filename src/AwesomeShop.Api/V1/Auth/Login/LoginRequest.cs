namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Auth.Login;

public sealed record LoginRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}