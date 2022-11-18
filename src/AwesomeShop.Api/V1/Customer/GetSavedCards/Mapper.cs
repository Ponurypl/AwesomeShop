using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetSavedCardDetails;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetSavedCards;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SavedCardDetailsDto, SavedCardDetails>();
    }
}