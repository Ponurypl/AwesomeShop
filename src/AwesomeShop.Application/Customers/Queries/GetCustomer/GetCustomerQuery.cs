namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed record GetCustomerQuery(string Username, string Password) : IQuery<CustomerDto>;