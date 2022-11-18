using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders;

public static class Failures
{
    public static Error InvalidUsername => new(nameof(InvalidUsername), "Invalid customer username");
    public static Error InvalidProduct => new(nameof(InvalidProduct), "Invalid product Id");
    public static Error InvalidQuantity => new(nameof(InvalidQuantity), "Invalid quantity");

    public static Error NoOpenCart =>
        new(nameof(NoOpenCart), "Given user doesn't have open cart");

    public static Error OrderNotExists => new(nameof(OrderNotExists), "Order doesn't exists");
    public static Error PaymentException => new(nameof(PaymentException), "There was problem with payment");
    public static Error InvalidToken => new (nameof(InvalidToken), "Invalid token");
}