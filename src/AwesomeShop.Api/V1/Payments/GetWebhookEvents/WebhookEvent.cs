﻿namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.GetWebhookEvents;

public sealed record WebhookEvent
{
    public Guid Id { get; init; }
    public string PaymentId { get; init; } = default!;
    public int? StatusCode { get; init; }
    public string Status { get; init; } = default!;
    public DateTime ReceivedAt { get; init; }
    public string Payload { get; init; } = default!;
}