namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryResponse(List<GetProductsByCategoryResponse.Product> Products)
{
    public record Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Availability { get; set; } = default!;
        public double Price { get; set; }
        public string? ThumbnailFileName { get; set; }
    }
}