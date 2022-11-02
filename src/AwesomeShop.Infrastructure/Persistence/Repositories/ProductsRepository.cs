using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class ProductsRepository : IProductsRepository
{
    private readonly DbSet<Product> _products;

    public ProductsRepository(ApplicationDbContext context)
    {
        _products = context.Set<Product>();
    }

    public async Task<List<Product>> GetByCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        return await _products.Where(p => p.CategoryId == categoryId).ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _products.SingleOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }
}