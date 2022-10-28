using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IPhotosRepository
{
    Task<Photo?> GetThumbnailByProductIdAsync(ProductId productId, CancellationToken cancellationToken = default);
    Task<List<Photo>> GetPhotosByProductIdAsync(ProductId productId, CancellationToken cancellationToken = default);
}