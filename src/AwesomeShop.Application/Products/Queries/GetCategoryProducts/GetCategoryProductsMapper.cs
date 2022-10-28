using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts;

public class GetCategoryProductsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AvailabilityStatus, AvailabilityStatusDto>();
        config.NewConfig<Product, ProductDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Ignore(d => d.ThumbnailFileName!);
    }
}