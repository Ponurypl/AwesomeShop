namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromOrder;

public sealed record RemoveProductFromOrderCommand(string Username, Guid OrderItemId) : ICommand;