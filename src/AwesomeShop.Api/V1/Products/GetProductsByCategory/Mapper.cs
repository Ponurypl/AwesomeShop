using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Products.GetProductsByCategory;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductDto, GetProductsByCategoryResponse.Product>()
              .Map(d => d.Availability, s => s.Availability.ToString());
    }
}