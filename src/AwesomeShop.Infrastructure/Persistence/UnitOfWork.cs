using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}