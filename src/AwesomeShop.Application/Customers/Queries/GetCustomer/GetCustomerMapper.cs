using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed class GetCustomerMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, CustomerDto>();
    }
}