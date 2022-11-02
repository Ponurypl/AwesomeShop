using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class PhotosRepository : IPhotosRepository
{
    private readonly DbSet<Photo> _photos;

    public PhotosRepository(ApplicationDbContext context)
    {
        _photos = context.Set<Photo>();
    }

    public async Task<Photo?> GetThumbnailByProductIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _photos.FirstOrDefaultAsync(p => p.ProductId == productId && p.IsThumbnailFormat,
                                                 cancellationToken);
    }

    public async Task<List<Photo>> GetPhotosByProductIdAsync(ProductId productId, CancellationToken cancellationToken = default)
    {
        return await _photos.Where(p => p.ProductId == productId).ToListAsync(cancellationToken);
    }
}