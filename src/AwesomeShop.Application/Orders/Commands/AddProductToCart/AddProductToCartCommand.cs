namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToCart;

public sealed record AddProductToCartCommand(string Username, Guid ProductId, int Quantity) : ICommand;