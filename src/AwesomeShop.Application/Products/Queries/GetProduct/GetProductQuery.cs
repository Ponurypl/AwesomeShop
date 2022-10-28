using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetProduct.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductDto>;