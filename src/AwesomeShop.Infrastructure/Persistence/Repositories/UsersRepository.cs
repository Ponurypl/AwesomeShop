using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class UsersRepository : IUsersRepository
{
    private readonly DbSet<User> _users;

    public UsersRepository(ApplicationDbContext context)
    {
        _users = context.Set<User>();
    }

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _users.SingleOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string emailAddress, CancellationToken cancellationToken = default)
    {
        return await _users.SingleOrDefaultAsync(u => u.EmailAddress == emailAddress, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _users.AddAsync(user, cancellationToken);
    }
}