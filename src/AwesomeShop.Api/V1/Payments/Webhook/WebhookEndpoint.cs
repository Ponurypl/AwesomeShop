using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Commands.AddWebhookEvent;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Webhooks;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Payments.Webhook;

public sealed class WebhookEndpoint : Endpoint<WebhookRequest>
{
    private readonly ISender _sender;

    public WebhookEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("payments/webhook");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(WebhookRequest req, CancellationToken ct)
    {
        var webhookHelper = Webhooks.CreateHelper(InMemorySecretKeyStore.Instance);
        var headers = HttpContext.Request.Headers.Select(h => new RequestHeader(h.Key, h.Value));
        try
        {
            var webhookEvent = webhookHelper.Unmarshal(req.Content, headers);
            var paymentId = webhookEvent.Payment.Id.Split("_")[0];

            await _sender.Send(new AddWebhookEventCommand(paymentId, webhookEvent.Payment.StatusOutput.StatusCode,
                                                          webhookEvent.Payment.Status,
                                                          DateTime.Parse(webhookEvent.Created), req.Content), ct);
        }
        catch (SignatureValidationException)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendOkAsync(ct);
    }
}