using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts;

public sealed record GetCategoryProductsQuery(Guid CategoryId) : IQuery<List<ProductDto>>;