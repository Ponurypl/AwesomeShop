namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToOrder;

public sealed record AddProductToOrderCommand(string Username, Guid ProductId, int Quantity) : ICommand;