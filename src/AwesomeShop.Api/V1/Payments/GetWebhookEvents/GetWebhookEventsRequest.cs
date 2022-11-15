namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.GetWebhookEvents;

public sealed record GetWebhookEventsRequest
{
    public string PaymentId { get; set; }
}