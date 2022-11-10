namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetOrder;

public sealed record OrderItemDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Summary { get; set; }
}