namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.Webhook;

public sealed record WebhookRequest : IPlainTextRequest
{
    public string Content { get; set; }
}