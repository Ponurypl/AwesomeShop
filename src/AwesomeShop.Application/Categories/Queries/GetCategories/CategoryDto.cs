namespace OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

public sealed class CategoryDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
}