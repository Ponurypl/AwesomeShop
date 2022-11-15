namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Orders.GetOrderById;

public sealed record Order
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public List<OrderItem> Items { get; set; } = new();
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
    public RecipientDetails Recipient { get; set; } = default!;
    public string? PaymentId { get; set; }
}