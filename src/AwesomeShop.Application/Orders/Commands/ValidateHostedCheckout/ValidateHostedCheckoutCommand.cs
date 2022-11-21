namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.ValidateHostedCheckout;

public sealed record ValidateHostedCheckoutCommand : ICommand<OrderDto>
{
    public Guid CheckoutId { get; set; }
    public string HostedPaymentId { get; set; }
}