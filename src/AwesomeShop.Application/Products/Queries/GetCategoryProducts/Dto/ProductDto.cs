namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;

public sealed class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public AvailabilityStatusDto Availability { get; set; }
    public double Price { get; set; }
    public PhotoDto? Thumbnail { get; set; }
    public List<PhotoDto> Photos { get; set; } = new();
}