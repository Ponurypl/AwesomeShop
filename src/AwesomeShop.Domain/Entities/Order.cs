using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public User Customer { get; }
    public IReadOnlyList<OrderItem> Items => _items;
    public OrderStatus Status { get; }
    public double Summary => Items.Sum(x => x.Summary);

    private Order(User customer) : base(OrderId.New())
    {
        Status = OrderStatus.Cart;
        Customer = customer;
    }

    public static Order Create(User customer)
    {
        return new Order(customer);
    }

    public void AddProduct(Product product, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Product == product);
        if (item is null)
        {
            item = OrderItem.Create(product, quantity);
            _items.Add(item);
        }
        else
        {
            item.ChangeQuantity(quantity);
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