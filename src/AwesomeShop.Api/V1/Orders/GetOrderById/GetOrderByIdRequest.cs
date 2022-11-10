namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;

public sealed record GetOrderByIdRequest
{
    public Guid OrderId { get; set; }
}