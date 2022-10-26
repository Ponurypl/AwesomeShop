using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

public sealed class GetCategoriesMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryDto>().Map(d => d.Id, s => s.Id.Value);
    }
}