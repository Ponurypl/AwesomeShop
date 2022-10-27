using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Product : Entity<ProductId>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AvailabilityStatus Availability { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; } = null!;
    public Photo? Thumbnail { get; set; }
    public List<Photo> Photos { get; set; } = new();

    public Product(ProductId id) : base(id)
    {
    }
}