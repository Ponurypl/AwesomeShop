namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

public sealed record CartItem(Guid OrderItemId, int Quantity);