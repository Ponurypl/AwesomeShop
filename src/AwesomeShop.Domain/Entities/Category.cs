using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Category 
{
    public CategoryId Id { get; set; }
    public string CategoryName { get; set; } = default!;
}