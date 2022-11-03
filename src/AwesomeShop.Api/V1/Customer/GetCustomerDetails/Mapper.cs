using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetCustomerDetails;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerDto, GetCustomerDetailsResponse>();
    }
}