using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class Order
{
    public OrderId Id { get; set; }
    public User Customer { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = new();
    public OrderStatus Status { get; set; }
}