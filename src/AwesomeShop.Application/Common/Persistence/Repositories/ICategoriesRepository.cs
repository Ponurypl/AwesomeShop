using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface ICategoriesRepository
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
}