namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries.GetWebhookEvents;

public sealed record GetWebhookEventsQuery(string PaymentId) : IQuery<List<WebHookEventDto>>;