using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IProductsRepository
{
    Task<List<Product>> GetByCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
}