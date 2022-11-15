using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Domain.ValueTypes;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Order : Entity<OrderId>
{
    private readonly List<OrderItem> _items = new();

    public UserId CustomerId { get; private set; }
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public OrderStatus Status { get; private set; }
    public double Summary { get; private set; }

    public string? Number { get; private set; }
    public RecipientDetails? Recipient { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string? PaymentId { get; private set; }


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

    public void AddOrderItem(ProductId productId, string productName, int quantity, double price)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item is null)
        {
            item = OrderItem.Create(productId, productName, quantity, price);
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

    public void ProcessCheckout(string number, string firstName, string lastName, string addressLine1, string? addressLine2,
                                string city, string zipCode, string phoneNumber, DateTime orderTime)
    {
        Number = number;
        Recipient = RecipientDetails.Create(firstName, lastName, addressLine1, addressLine2, city, zipCode,
                                            phoneNumber);
        Status = OrderStatus.Created;
        CreationDate = orderTime;
    }

    private void RecalculateSummary()
    {
        Summary = Items.Sum(x => x.Summary);
    }

    public void AddPaymentId(string paymentId)
    {
        PaymentId = paymentId;
    }

    public void SetWaitingForPaymentStatus()
    {
        Status = OrderStatus.WaitingForPayment;
    }

    public void SetPaidStatus()
    {
        Status = OrderStatus.Paid;
    }
}