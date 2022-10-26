namespace OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery() : IQuery<List<CategoryDto>>;