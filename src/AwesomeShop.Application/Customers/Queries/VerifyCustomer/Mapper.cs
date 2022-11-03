using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.VerifyCustomer;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, VerifyCustomerDto>().Map(d => d.Id, s => s.Id.Value);
    }
}