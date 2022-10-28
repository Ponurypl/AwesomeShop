using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public UserId CustomerId { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items;
    public OrderStatus Status { get; private set; }
    public double Summary => Items.Sum(x => x.Summary);

    private Order(UserId customerId) 
        : base(OrderId.New())
    {
        Status = OrderStatus.Cart;
        CustomerId = customerId;
    }

    public static Order Create(UserId customerId)
    {
        return new Order(customerId);
    }

    public void AddProduct(ProductId productId, int quantity, double price)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            item = OrderItem.Create(productId, quantity, price);
            _items.Add(item);
        }
        else
        {
            item.ChangeQuantity(quantity, price);
        }
    }

    public void RemoveProduct(OrderItemId orderItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == orderItemId);
        if (item is not null)
        {
            _items.Remove(item);
        }
    }
}