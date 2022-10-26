namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}