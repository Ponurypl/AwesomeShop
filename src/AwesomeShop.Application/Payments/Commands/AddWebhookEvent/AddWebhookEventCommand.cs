namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Commands.AddWebhookEvent;

public sealed record AddWebhookEventCommand(string PaymentId, int? StatusCode, string Status, DateTime ReceivedAt,
                                            string Payload) : ICommand;