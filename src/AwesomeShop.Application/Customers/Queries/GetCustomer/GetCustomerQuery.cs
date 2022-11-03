namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed record GetCustomerQuery(Guid UserId) : IQuery<CustomerDto>;