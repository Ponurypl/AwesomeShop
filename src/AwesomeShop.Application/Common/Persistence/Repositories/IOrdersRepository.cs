using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IOrdersRepository
{
    Task<Order?> GetCartOrderByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    void Remove(Order order);
    Task<List<Order>> GetOrdersByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAndUserId(OrderId orderId, UserId userId, CancellationToken cancellationToken = default);
    Task<int> GetNumberOfOrdersFromMonth(int month, int year, CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByPaymentIdAsync(string paymentId, CancellationToken cancellationToken = default);
}