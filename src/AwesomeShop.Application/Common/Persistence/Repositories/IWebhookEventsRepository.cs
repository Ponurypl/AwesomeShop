using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

public interface IWebhookEventsRepository
{
    Task AddAsync(WebHookEvent webHookEvent, CancellationToken cancellationToken = default);
    Task<List<WebHookEvent>> GetAllByPaymentIdAsync(string paymentId, CancellationToken cancellationToken = default);
}