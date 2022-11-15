namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Commands;

public sealed record AddWebhookEventCommand(string PaymentId, int? StatusCode, string Status, DateTime ReceivedAt, 
                                            string Payload) : ICommand;