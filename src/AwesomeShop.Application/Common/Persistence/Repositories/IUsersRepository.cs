using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Add(User user);
}