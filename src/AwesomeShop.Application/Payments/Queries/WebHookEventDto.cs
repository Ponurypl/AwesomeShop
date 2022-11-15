namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Queries;

public sealed record WebHookEventDto
{
    public Guid Id { get; init; }
    public string PaymentId { get; init; }
    public int? StatusCode { get; init; }
    public string Status { get; init; }
    public DateTime ReceivedAt { get; init; }
    public string Payload { get; init; }
}