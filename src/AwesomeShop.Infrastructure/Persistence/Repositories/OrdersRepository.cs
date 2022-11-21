using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Enums;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class OrdersRepository : IOrdersRepository
{
    private readonly DbSet<Order> _orders;

    public OrdersRepository(ApplicationDbContext context)
    {
        _orders = context.Set<Order>();
    }

    public async Task<Order?> GetCartOrderByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _orders.FirstOrDefaultAsync(o => o.CustomerId == userId && o.Status == OrderStatus.Cart,
                                                 cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _orders.AddAsync(order, cancellationToken);
    }

    public void Remove(Order order)
    {
        _orders.Remove(order);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _orders.Where(o => o.CustomerId == userId && o.Status != OrderStatus.Cart)
                            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByIdAndUserId(OrderId orderId, UserId userId, CancellationToken cancellationToken = default)
    {
        return await _orders.FirstOrDefaultAsync(o => o.Id == orderId && o.CustomerId == userId, cancellationToken);
    }

    public async Task<int> GetNumberOfOrdersFromMonth(int month, int year, CancellationToken cancellationToken = default)
    {
        DateTime begin = new(year, month, 1);
        DateTime end = begin.AddMonths(1);
        return await _orders.Where(o => o.Status != OrderStatus.Cart && 
                                        o.CreationDate >= begin &&
                                        o.CreationDate < end)
                            .CountAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByPaymentIdAsync(string paymentId, CancellationToken cancellationToken = default)
    {
        return await _orders.FirstOrDefaultAsync(o => o.PaymentId == paymentId, cancellationToken);
    }

    public async Task<Order?> GetOrderByCheckoutIdAndHostedPaymentIdAsync(Guid checkoutId, string hostedPaymentId,
                                                                    CancellationToken cancellationToken = default)
    {
        return await _orders.FirstOrDefaultAsync(o => o.HostedPaymentId == hostedPaymentId && o.CheckoutId == checkoutId,
                                            cancellationToken);
    }
}