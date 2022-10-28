using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Photo : Entity<PhotoId>
{
    public string FileName { get; set; } = default!;
    public ProductId ProductId { get; set; } 
    public bool IsThumbnailFormat { get; set; }

    public Photo(PhotoId id) : base(id)
    {
    }
}