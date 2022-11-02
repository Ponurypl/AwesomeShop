namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToCart;

public sealed record AddProductToCartCommand(Guid UserId, Guid ProductId, int Quantity) : ICommand;