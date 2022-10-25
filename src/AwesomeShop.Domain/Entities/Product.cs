using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Product
{
    public ProductId Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AvailabilityStatus Availability { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; } = null!;
    public Photo? Thumbnail { get; set; }
    public List<Photo> Photos { get; set; } = new();
}