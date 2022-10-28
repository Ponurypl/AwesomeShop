namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromCart;

public sealed record RemoveProductFromCartCommand(string Username, Guid OrderItemId) : ICommand;