namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class OrderItem
{
    public OrderItemId Id { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Summary { get; set; }
}