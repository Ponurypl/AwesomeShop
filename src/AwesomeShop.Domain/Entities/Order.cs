using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public UserId CustomerId { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public OrderStatus Status { get; private set; }
    public double Summary { get; private set; }
    

    private Order(OrderId id ,UserId customerId) 
        : base(id)
    {
        Status = OrderStatus.Cart;
        CustomerId = customerId;
    }

    public static Order Create(UserId customerId)
    {
        return new Order(OrderId.New(), customerId);
    }

    public void AddOrderItem(ProductId productId, int quantity, double price)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            item = OrderItem.Create(productId, quantity, price);
            _items.Add(item);
        }
        else
        {
            item.OffsetQuantity(quantity);
        }
        RecalculateSummary();
    }

    public void RemoveOrderItem(OrderItemId orderItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (item is not null)
        {
            _items.Remove(item);
        }
        RecalculateSummary();
    }

    public void ChangeOrderItem(OrderItemId orderItemId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Id == orderItemId);
        item?.OverrideQuantity(quantity);
        RecalculateSummary();
    }

    private void RecalculateSummary()
    {
        Summary = Items.Sum(x => x.Summary);
    }
}