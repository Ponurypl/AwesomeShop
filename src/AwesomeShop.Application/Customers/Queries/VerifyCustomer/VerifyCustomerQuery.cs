namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.VerifyCustomer;

public sealed record VerifyCustomerQuery(string Username, string Password) : IQuery<VerifyCustomerDto>;