namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class OrderDetailsItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Summary { get; set; }
}