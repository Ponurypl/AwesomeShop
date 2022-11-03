using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public double Price { get; private set; }
    public double Summary { get; private set; }

    private OrderItem(OrderItemId id, ProductId productId, string productName, int quantity, double price) 
        : base(id)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price; 
        RecalculateSummary();
    }

    internal static OrderItem Create(ProductId productId, string productName, int quantity, double price)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));

        return new OrderItem(OrderItemId.New(), productId, productName, quantity, price);
    }

    public void OffsetQuantity(int offset)
    {
        if (offset == 0) throw new ArgumentOutOfRangeException(nameof(offset));
        Quantity += offset;
        RecalculateSummary();
    }

    public void OverrideQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        Quantity = quantity;

        RecalculateSummary();
    }

    private void RecalculateSummary()
    {
        Summary = Price * Quantity;
    }
}