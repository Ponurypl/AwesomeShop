namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetProduct.Dto;

public sealed class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AvailabilityStatusDto Availability { get; set; }
    public double Price { get; set; }
    public string? ThumbnailFileName { get; set; }
    public List<string> PhotoFileNames { get; set; } = new();
}