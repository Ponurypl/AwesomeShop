using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Photo
{
    public PhotoId Id { get; set; }
    public string FileName { get; set; } = default!;
    public Product Product { get; set; } = null!;
    public bool IsThumbnailFormat { get; set; }
}