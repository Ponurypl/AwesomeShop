using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IProductsRepository
{
    Task<List<Product>> GetByCategoryAsync(Guid categoryId);
}