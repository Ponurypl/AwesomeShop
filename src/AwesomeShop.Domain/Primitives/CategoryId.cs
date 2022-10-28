namespace OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct CategoryId
{
    
}