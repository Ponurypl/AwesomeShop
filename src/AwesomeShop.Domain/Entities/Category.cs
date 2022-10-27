using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Category : Entity<CategoryId>
{
    public string CategoryName { get; set; } = default!;

    public Category(CategoryId id) : base(id)
    {
    }
}