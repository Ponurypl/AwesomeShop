namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.UpdateProductsInCart;

public sealed record UpdateProductsInCartCommand(Guid UserId,
    List<UpdateProductsInCartCommand.CartItem> ItemsToChange) : ICommand
{
    public sealed record CartItem(Guid OrderItemId, int Quantity);
}