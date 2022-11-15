namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public sealed class WebhookEvent
{
    public Guid Id { get; set; }
    public string PaymentId { get; set; }
    public int? StatusCode { get; set; }
    public string Status { get; set; }
    public DateTimeOffset ReceivedAt { get; set; }
    public string Payload { get; set; }
    public bool ShowDetails { get; set; }
}