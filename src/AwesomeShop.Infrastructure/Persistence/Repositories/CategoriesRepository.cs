using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class CategoriesRepository : ICategoriesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Category> _categories;

    public CategoriesRepository(ApplicationDbContext context)
    {
        _context = context;
        _categories = context.Set<Category>();
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _categories.ToListAsync(cancellationToken);
    }
}