using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
    public Product Product { get; }
    public int Quantity { get; private set; }
    public double Price { get; }
    public double Summary => Price * Quantity;

    private OrderItem(Product product, int quantity) : base(OrderItemId.New())
    {
        Product = product;
        Quantity = quantity;
        Price = product.Price;
    }

    internal static OrderItem Create(Product product, int quantity)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

        return new OrderItem(product, quantity);
    }

    public void ChangeQuantity(int offset)
    {
        Quantity += offset;
    }
}