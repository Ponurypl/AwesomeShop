using OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Categories.GetCategories;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CategoryDto, GetCategoriesResponse.Category>();
    }
}