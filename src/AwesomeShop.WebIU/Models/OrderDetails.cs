namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class OrderDetails
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public List<OrderDetailsItem> Items { get; set; } = new();
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
    public RecipientDetails Recipient { get; set; } = default!;
}