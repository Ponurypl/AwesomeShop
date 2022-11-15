using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives.Results;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class WebHookEvent : Entity<WebHookEventId>
{
    public string PaymentId { get; private set; }
    public int? StatusCode { get; private set; }
    public string Status { get; private set; }
    public DateTime ReceivedAt { get; private set; }
    public string Payload { get; private set; }

    private WebHookEvent(WebHookEventId id, string paymentId, int? statusCode, string status, DateTime receivedAt,
                         string payload) : base(id)
    {
        PaymentId = paymentId;
        StatusCode = statusCode;
        Status = status;
        ReceivedAt = receivedAt;
        Payload = payload;
    }

    public static Result<WebHookEvent> Create(string paymentId, int? statusCode, string status, DateTime receivedAt,
                                      string payload)
    {
        return new WebHookEvent(WebHookEventId.New(), paymentId, statusCode, status, receivedAt, payload);
    }
}