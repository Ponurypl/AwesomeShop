using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IOrdersRepository
{
    Task<Order?> GetCartOrderByUsernameAsync(string username, CancellationToken cancellationToken = default);
    void Add(Order order);
}