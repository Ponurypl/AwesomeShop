namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.RemoveProductFromCart;

public sealed record RemoveProductFromCartCommand(Guid UserId, Guid OrderItemId) : ICommand;