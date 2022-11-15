namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries;

public sealed record GetWebhookEventsQuery(string PaymentId) : IQuery<List<WebHookEventDto>>;