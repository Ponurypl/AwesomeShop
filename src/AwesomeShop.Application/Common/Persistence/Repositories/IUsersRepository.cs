using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IUsersRepository
{
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default); 
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string emailAddress, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}