namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers;

public static class Failures
{
    public static Error UsernameAlreadyTaken => new(nameof(UsernameAlreadyTaken), "Username is already taken");
    public static Error EmailAlreadyTaken => new(nameof(EmailAlreadyTaken), "Email is already taken");
    public static Error NotExistingUser => new(nameof(NotExistingUser), "Username is not existing in database");
}