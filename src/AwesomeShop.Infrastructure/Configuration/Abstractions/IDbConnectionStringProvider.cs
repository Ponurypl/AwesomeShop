namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.Abstractions;

public interface IDbConnectionStringProvider
{
    string ConnectionString { get; }
}