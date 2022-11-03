namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Auth.Login;

public sealed record LoginResponse(string Token, DateTimeOffset ExpireAt);