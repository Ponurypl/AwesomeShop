using Microsoft.EntityFrameworkCore;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Persistence.Repositories;

internal sealed class WebhookEventRepository : IWebhookEventsRepository
{
    private readonly DbSet<WebHookEvent> _events;

    public WebhookEventRepository(ApplicationDbContext context)
    {
        _events = context.Set<WebHookEvent>();
    }

    public async Task AddAsync(WebHookEvent webHookEvent, CancellationToken cancellationToken = default)
    {
        await _events.AddAsync(webHookEvent, cancellationToken);
    }

    public async Task<List<WebHookEvent>> GetAllByPaymentIdAsync(string paymentId, CancellationToken cancellationToken = default)
    {
        return await _events.Where(e => e.PaymentId == paymentId)
                            .OrderBy(e => e.ReceivedAt)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
    }
}