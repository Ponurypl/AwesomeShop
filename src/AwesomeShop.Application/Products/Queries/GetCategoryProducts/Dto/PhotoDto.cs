namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;

public sealed class PhotoDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = default!;
}