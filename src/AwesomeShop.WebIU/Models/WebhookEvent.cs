namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class WebhookEvent
{
    public Guid Id { get; set; }
    public string PaymentId { get; set; } = default!;
    public int? StatusCode { get; set; }
    public string Status { get; set; } = default!;
    public DateTimeOffset ReceivedAt { get; set; }
    public string Payload { get; set; } = default!;
    public bool ShowDetails { get; set; }
}