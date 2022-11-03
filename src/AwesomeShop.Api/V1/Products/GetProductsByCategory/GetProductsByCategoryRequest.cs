namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Products.GetProductsByCategory;

public sealed record GetProductsByCategoryRequest
{
    public Guid CategoryId { get; set; }
}