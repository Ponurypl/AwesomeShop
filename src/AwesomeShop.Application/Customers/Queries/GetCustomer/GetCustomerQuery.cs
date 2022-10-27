namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed record GetCustomerQuery(string Username) : IQuery<CustomerDto>;