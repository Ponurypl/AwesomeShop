using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public double Price { get; private set; }
    public double Summary => Price * Quantity;

    private OrderItem(ProductId productId, int quantity, double price) 
        : base(OrderItemId.New())
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    internal static OrderItem Create(ProductId productId, int quantity, double price)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));

        return new OrderItem(productId, quantity, price);
    }

    public void OffsetQuantity(int offset)
    {
        if (offset == 0) throw new ArgumentOutOfRangeException(nameof(offset));
        Quantity += offset;
    }

    public void OverrideQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        Quantity = quantity;
    }
}