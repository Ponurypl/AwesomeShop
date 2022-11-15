using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class CategoriesRepository : ICategoriesRepository
{
    private readonly DbSet<Category> _categories;

    public CategoriesRepository(ApplicationDbContext context)
    {
        _categories = context.Set<Category>();
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _categories.ToListAsync(cancellationToken);
    }
}