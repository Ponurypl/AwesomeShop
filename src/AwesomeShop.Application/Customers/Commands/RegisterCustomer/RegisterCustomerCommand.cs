namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Commands.RegisterCustomer;

public record RegisterCustomerCommand(string Username, string Password, string FirstName, string LastName, string EmailAddress) : ICommand;