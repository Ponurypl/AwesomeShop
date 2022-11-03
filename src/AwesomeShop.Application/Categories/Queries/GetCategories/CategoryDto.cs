namespace OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

public sealed record CategoryDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
}