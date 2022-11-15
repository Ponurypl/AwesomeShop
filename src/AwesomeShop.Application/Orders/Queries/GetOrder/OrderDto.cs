namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed record OrderDto
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public List<OrderItemDto> Items { get; set; } = new();
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
    public RecipientDetailsDto Recipient { get; set; } = default!;
    public string PaymentId { get; set; }
}